using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerVisual : MonoBehaviour {
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private const string IS_RUNNING = "IsRunning";
    private const string IS_DASHING = "IsDashing";

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        animator.SetBool(IS_DASHING, Player.Instance.IsDashing());
        AdjustPlayerFacingDirection();
    }

    private void AdjustPlayerFacingDirection() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

        if (mousePos.x < playerPosition.x) {
            spriteRenderer.flipX = true;
        }
        else {
            spriteRenderer.flipX = false;
        }
    }
}
