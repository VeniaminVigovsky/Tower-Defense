using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomby : Enemy
{

    private float jumpPower;
    private float jumpDuration;
    private Renderer[] mats;
    private float dissolveStartValue;
    private float spawnDuration;
    private List<Node> nodePath;
    private void OnEnable()
    {
        mats = graphics.GetComponentsInChildren<Renderer>();
        dissolveStartValue = 1;
        spawnDuration = 1f;
        foreach (var mat in mats)
        {
            
            mat.material.SetFloat("_Dissolve", dissolveStartValue);
        }
        
        if (aStar == null)
        {
            aStar = new AStar(grid);
            
        }

        nodePath = aStar.GetNodePath(transform.position, tower.transform.position);

        if (nodePath != null)
        {
            StartCoroutine(MoveByNodes(nodePath));
        }
    }

    public override void EnemyConstructor(NodeGrid grid, Tower tower, int id)
    {
        base.EnemyConstructor(grid, tower, id);
        jumpPower = 1f;
        jumpDuration = 0.6f;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        int kills = transform.DOKill();
        foreach (var mat in mats)
        {
            mat.material.SetFloat("_Dissolve", dissolveStartValue);
        }
        //StopAllCoroutines();

    }

    public override void DestroySelf()
    {
        foreach (var mat in mats)
        {
            mat.material.SetFloat("_Dissolve", dissolveStartValue);
        }
        base.DestroySelf();
    }

    public override void OnTowerReached(Collider other)
    {
        StopCoroutine(MoveByNodes(nodePath));
        base.OnTowerReached(other);
    }




    private IEnumerator MoveByNodes(List<Node> nodes)
    {
        foreach (var mat in mats)
        {
            mat.material.DOFloat(-0.35f, "_Dissolve", spawnDuration);
        }
        
        yield return new WaitForSeconds(spawnDuration);

        foreach (var n in nodes)
        {

            transform.DOJump(new Vector3(n.WorldPosition.x, transform.position.y, n.WorldPosition.z), jumpPower, 1, jumpDuration);
            //transform.position = new Vector3(n.WorldPosition.x, transform.position.y, n.WorldPosition.z);
            yield return new WaitForSeconds(jumpDuration);
        }
    }


}
