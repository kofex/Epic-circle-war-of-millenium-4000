using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Tests
{
    public class PhysicsTest : MonoBehaviour
    {

        public List<Unit> Units;
        public List<Border> Borders; // l r t b

        private Vector4 Border; // x min x max y min y max
    
        void Start()
        {
            SetBorders();
        }

        private void SetBorders()
        {
            Border.x = Borders[0].transform.position.x;
            Border.y = Borders[1].transform.position.x;
            Border.z = Borders[2].transform.position.y;
            Border.w = Borders[3].transform.position.y;
        }
    
        void Update()
        {
            CheckBorderCollision();
            CheckUnitsCollision();
        }

        private void CheckBorderCollision()
        {
            foreach (var unit in Units)
            {
                var unitPos = unit.transform.position;
                var resVec = Vector2.zero;

                var  range = Random.Range(-0.3f, 0.3f);
                if (unitPos.x - unit.Radius < Border.x)
                {
                    resVec += new Vector2(1f, range);
                }
                if (unitPos.x + unit.Radius > Border.y)
                {
                    resVec += new Vector2(-1f, range);
                }

                if(unitPos.y - unit.Radius <= Border.z)
                {
                    resVec += new Vector2(range,1f);
                }

                if (unitPos.y + unit.Radius >= Border.w)
                {
                    resVec += new Vector2(range,-1f); 
                }


                if (resVec != Vector2.zero)
                {
                    unit.OnCollision(resVec.normalized);
                }
            }
        }

        private void CheckUnitsCollision()
        {
            foreach (var unit in Units)
            {
                foreach (var target in Units)
                {
                    if(unit == target)
                        continue;

                    var unitPos = unit.transform.position;
                    var targetPost = target.transform.position;
                    var x = unitPos.x - targetPost.x;
                    var y = unitPos.y - targetPost.y;
                    var dist = Mathf.Sqrt(x * x + y * y);
                    var radiiSum = unit.Radius + target.Radius;
                    if (dist > radiiSum)
                        continue;

                    unit.OnCollision(new Vector2(x, y).normalized);
                    target.OnCollision(new Vector2(-x, -y).normalized);
                }
            }
        }
    }
}