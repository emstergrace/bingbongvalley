using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TheKiwiCoder {

    // The context is a shared object every node has access to.
    // Commonly used components and subsytems should be stored here
    // It will be somewhat specfic to your game exactly what to add here.
    // Feel free to extend this class 
    public class Context {
        public GameObject gameObject;
        public Transform transform;
        public Animator animator;
        public Rigidbody2D physics;
        public NavMeshAgent agent;
        public CircleCollider2D circleCollider;
        public BoxCollider2D boxCollider;
        public CapsuleCollider2D capsuleCollider;
        public CharacterController characterController;
        public BaseEntity baseEntity;
        public BaseMoveController moveController;
        public BaseCombatController combatController;
        public AIPerception perception;
        // Add other game specific systems here

        public static Context CreateFromGameObject(GameObject gameObject) {
            // Fetch all commonly used components
            Context context = new Context();
            context.gameObject = gameObject;
            context.transform = gameObject.transform;
            context.animator = gameObject.GetComponent<Animator>();
            context.physics = gameObject.GetComponent<Rigidbody2D>();
            context.agent = gameObject.GetComponent<NavMeshAgent>();
            context.circleCollider = gameObject.GetComponent<CircleCollider2D>();
            context.boxCollider = gameObject.GetComponent<BoxCollider2D>();
            context.capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
            context.characterController = gameObject.GetComponent<CharacterController>();
            context.baseEntity = gameObject.GetComponent<BaseEntity>();
            context.moveController = gameObject.GetComponent<BaseMoveController>();
            context.combatController = gameObject.GetComponent<BaseCombatController>();
            context.perception = gameObject.GetComponent<AIPerception>();

            return context;
        }
    }
}