using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached => this._agent != null && this._agent.IsReached;

        private MoveAgent _agent;
        private IMover _mover;

        public void Construct(MoveAgent agent)
        {
            this._agent = agent;
            this._mover = this.GetComponent<IMover>();
        }

        public void SetDestination(Vector2 endPoint)
        {
            this._agent.SetDestination(endPoint);
        }

        private void FixedUpdate()
        {
            if (this._agent == null)
            {
                return;
            }

            Vector2 direction = this._agent.NextDirection(this.transform.position);
            if (direction.sqrMagnitude > 0.0f)
            {
                this._mover.Move(direction * Time.fixedDeltaTime);
            }
        }
    }
}
