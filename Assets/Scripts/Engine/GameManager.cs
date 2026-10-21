using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }
    public enum GameState {
        Combat,
        CombatOver,
    }

    //TODO: This is temporary, need to load characters from a save file or something similar
    [SerializeField] private List<Character> playerCharacters;
    [SerializeField] private List<Character> enemyCharacters;

    public event EventHandler<DamageEventArgs> OnActionEvent;
    public class DamageEventArgs : EventArgs {
        public Character target;

        public DamageEventArgs(Character target) {
            this.target = target;
        }
    }
    public event EventHandler<CombatOverEventArgs> OnCombatOverEvent;
    public class CombatOverEventArgs : EventArgs {
        public bool playerWon;
        public CombatOverEventArgs(bool playerWon) {
            this.playerWon = playerWon;
        }
    }
    public event EventHandler<Character> OnTurnChangedEvent;

    private Queue<Character> turnQueue;
    private Character currentActor;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
        turnQueue = new Queue<Character>();
        QueueActors();
        NextActor();
    }

    private void Start() {
        CombatUI.Instance.OnAttackButtonClickedEvent += OnAttackButtonClicked;
        CombatUI.Instance.OnSkillSelectedEvent += OnSkillSelected;

        CurrentState = GameState.Combat;
    }

    private void OnSkillSelected(object sender, SkillData e) {
        if (CurrentState == GameState.CombatOver) {
            Debug.LogWarning("Combat is over, cannot use skills!");
            return;
        }
        Debug.Log($"Skill selected: {e.skillName} by {currentActor.GetCharacterName()}!");
        SpellAttackCycle(e);
    }

    private void OnAttackButtonClicked(object sender, EventArgs e) {
        if (CurrentState == GameState.CombatOver) {
            Debug.LogWarning("Combat is over, cannot attack!");
            return;
        }
        Debug.Log($"Attack button clicked by {currentActor.GetCharacterName()}!");
        BasicAttackCycle();
    }

    private void OnDestroy() {
        if (CombatUI.Instance != null) {
            CombatUI.Instance.OnAttackButtonClickedEvent -= OnAttackButtonClicked;
            CombatUI.Instance.OnSkillSelectedEvent -= OnSkillSelected;
        }
    }

    private void NextActor() {
        CheckCombatOver();
        if (CurrentState == GameState.CombatOver) {
            return;
        }
        while (turnQueue.Count > 0 && turnQueue.Peek().GetCurrentHP() <= 0) {
            turnQueue.Dequeue(); // Remove dead characters from the queue
        }
        if (turnQueue.Count > 0) {
            currentActor = turnQueue.Dequeue();
        } else {
            QueueActors();
            currentActor = turnQueue.Dequeue();
        }
        Debug.Log($"It's {currentActor.GetCharacterName()}'s turn!");
        OnTurnChangedEvent?.Invoke(this, currentActor);
    }

    private void QueueActors() {
        //TODO: Need to load characters based on speed or some stat
        foreach (Character character in playerCharacters) {
            if (character.GetCurrentHP() > 0) {
                turnQueue.Enqueue(character);
            }
        }
        foreach (Character character in enemyCharacters) {
            if (character.GetCurrentHP() > 0) {
                turnQueue.Enqueue(character);
            }
        }
    }

    private void BasicAttackCycle() {
        Character target = ChooseTarget(currentActor);

        if (target == null) {
            Debug.LogWarning("No valid target found for attack!");
            return;
        }

        Debug.Log($"{currentActor.GetCharacterName()} attacks {target.GetCharacterName()}! " +
            $"Hits them with {currentActor.GetBasicAttack().skillName}!");
        DoDamage(target, currentActor.GetBasicAttack());
    }

    private void SpellAttackCycle(SkillData skill) {
        // TODO: Need to select target based on player input or AI logic
        Character target = ChooseTarget(currentActor);

        if (target == null) {
            Debug.LogWarning("No valid target found for skill!");
            return;
        }

        if (skill.targetType == SkillTargetType.Ally) {
            target = currentActor; // For now, just target self for ally skills
        }
        DoDamage(target, skill);
    }

    private void DoDamage(Character target, SkillData skill) {
        if (SkillResolver.ResolveSkill(currentActor, target, skill)) {
            OnActionEvent?.Invoke(this, new DamageEventArgs(target));
            NextActor();
        }
    }

    private Character ChooseTarget(Character attacker) {
        Character target = null;
        if (playerCharacters.Contains(attacker)) {
            target = enemyCharacters.Find(c => c.GetCurrentHP() > 0); //TODO: Need to select target based on player input
        } else {
            target = playerCharacters.Find(c => c.GetCurrentHP() > 0); //TODO: Need to select target based on AI logic
        }
        return target;
    }

    private void CheckCombatOver() {
        if (CurrentState == GameState.CombatOver) {
            return;
        }
        bool playerCharactersAlive = false;
        foreach (Character character in playerCharacters) {
            if (character.GetCurrentHP() > 0) {
                playerCharactersAlive = true;
                break;
            }
        }

        bool enemyCharactersAlive = false;
        foreach (Character character in enemyCharacters) {
            if (character.GetCurrentHP() > 0) {
                enemyCharactersAlive = true;
                break;
            }
        }

        if (!playerCharactersAlive) {
            Debug.Log("All player characters have been defeated!");
            // Handle player defeat
            CombatOver();
            OnCombatOverEvent?.Invoke(this, new CombatOverEventArgs(false));
        } else if (!enemyCharactersAlive) {
            Debug.Log("All enemy characters have been defeated!");
            // Handle enemy defeat
            CombatOver();
            OnCombatOverEvent?.Invoke(this, new CombatOverEventArgs(true));
        }
    }

    private void CombatOver() {
        CurrentState = GameState.CombatOver;
        turnQueue.Clear();
    }

    public Character GetCurrentActor() {
        return currentActor;
    }
}
