using UnityEngine;

namespace Geekbrains
{
    [System.Serializable]
    public sealed class Vision
    {
        public float ActiveDis = 10;
        public float ActiveAng = 35;
        public float LostDistance = 40;

        public bool VisionM(Transform player, Transform target)
        {
            return Dist(player, target) && Angle(player, target) && !CheckBlocked(player, target);
        }

        public bool CheckOnLoss(Transform player, Transform target)
        {
            return Dist(player, target, true);
        }

        private bool CheckBlocked(Transform player, Transform target)
        {
            if (!Physics.Linecast(player.position, target.position, out var hit))
            {
                return true;
            }
            return hit.transform != target;
        }
        private bool Angle(Transform player, Transform target)
        {
            var angle = Vector3.Angle(player.forward, target.position - player.position);
            return angle <= ActiveAng;
        }
        private bool Dist(Transform player, Transform target, bool isLost = false)
        {
          var dist = (player.position - target.position).sqrMagnitude;
          if (isLost)
          {
              return dist >= LostDistance * LostDistance;
          }
          return dist <= ActiveDis * ActiveDis;
            //var dist = Vector3.Distance(player.position, target.position); //todo оптимизация
            //return dist <= ActiveDis;
        }
        
        
        
    }
}