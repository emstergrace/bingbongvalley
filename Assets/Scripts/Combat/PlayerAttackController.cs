using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : BaseCombatController
{

    public static bool isAiming { get; private set; } = false;

    private InputAction attackAction;
    private bool attackQueued = false;
    public GameObject meleeGO = null;
    public Vector3 aimDirection { get; private set; } = Vector3.zero;

    // Start is called before the first frame update
    public override void Start() {
		base.Start();

		attackAction = GameInputManager.GameplayMap.FindAction("Attack");

		attackAction.started += HandleInput;
		attackAction.canceled += HandleInput;

        GameInputManager.GameplayMap.FindAction("Aim").performed += HandleAimInput;
    } // End of Start().

    public override void Update() {
        base.Update();

        if (timeCounter <= 0f && attackQueued & attackAction.IsPressed()) {
            DoAttack();
        }

        if (GameInputManager.ActiveGamepad == null) {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            UpdateAimDirection(new Vector3(mousePoint.x, mousePoint.y, 0f) - transform.position);
        }
    } // End of Update().

    private void HandleAimInput(InputAction.CallbackContext context) {
        UpdateAimDirection(context.ReadValue<Vector2>());
	}

    public void UpdateAimDirection(Vector2 direction) {
        aimDirection = direction;
	} // End of UpdateAimDirection().

	public void HandleInput(InputAction.CallbackContext context) {
        if (GameManager.TopmostMenu != null) return;
        
        if (context.action == attackAction) {
            if (timeCounter <= 0f) {
                if (context.started || (attackQueued && context.action.IsPressed())){
                    DoAttack();
				}
			}

            if (context.canceled) {
                attackQueued = false;
                consecutiveShots = 0;
			}
		}

	} // End of HandleInput().

    private void DoAttack() {
        Attack();
        if (anim) anim.SetBool("Attack", true);
        timeCounter = attackTimer;
		attackQueued = true;
	} // End of DoAttack().

	public override void Attack() {
        StartCoroutine(MeleeCorout());
	} // End of Attack().

    private IEnumerator MeleeCorout() {
        yield return null;
        meleeGO.SetActive(true);
        meleeGO.transform.localPosition = aimDirection.normalized * 0.6f;
        meleeGO.transform.up = aimDirection.normalized;
        yield return new WaitForSeconds(attackTimer - 0.1f);
        meleeGO.SetActive(false);
	}

} // End of PlayerAttackController.
