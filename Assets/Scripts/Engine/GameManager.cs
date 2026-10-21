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

    public event EventHandler<DamageEventArgs> OnDamageEvent;
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

    private Queue<Character> turnQueue;
    private Character currentActor;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        CombatUI.Instance.OnAttackButtonClickedEvent += OnAttackButtonClicked;

        CurrentState = GameState.Combat;

        turnQueue = new Queue<Character>();
        QueueActors();
        NextActor();

    }

    private void OnAttackButtonClicked(object sender, EventArgs e) {
        if (CurrentState == GameState.CombatOver) {
            Debug.LogWarning("Combat is over, cannot attack!");
            return;
        }
        Debug.Log($"Attack button clicked by {currentActor.GetCharacterName()}!");
        BasicAttackCycle();
        NextActor();
    }

    private void OnDestroy() {
        if (CombatUI.Instance != null) {
            CombatUI.Instance.OnAttackButtonClickedEvent -= OnAttackButtonClicked;
        }
    }

    private void NextActor() {
        CheckCombatOver();
        if (CurrentState == GameState.CombatOver) {
            return;
        }
        if (turnQueue.Count > 0) {
            currentActor = turnQueue.Dequeue();
        } else {
            QueueActors();
            currentActor = turnQueue.Dequeue();
        }
        Debug.Log($"It's {currentActor.GetCharacterName()}'s turn!");
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
        Character attacker = currentActor;
        Character target = ChooseTarget(currentActor);

        if (target == null) {
            Debug.LogWarning("No valid target found for attack!");
            return;
        }

        Debug.Log($"{attacker.GetCharacterName()} attacks {target.GetCharacterName()}! " +
            $"Hits them with {attacker.GetBasicAttack().skillName}!");

        target.TakeDamage(CalculateDamage(attacker.GetBasicAttack(), attacker));
        OnDamageEvent?.Invoke(this, new DamageEventArgs(target));
    }

    private int CalculateDamage(SkillData skill, Character attacker) {
        //Placeholder damage calculation, will be replaced with a more complex formula later
        Debug.Log($"Calculating damage for {attacker.GetCharacterName()} using skill {skill.skillName} with power {skill.power} and attack power {attacker.GetAttackPower()}");
        return skill.power * attacker.GetAttackPower();
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
}
