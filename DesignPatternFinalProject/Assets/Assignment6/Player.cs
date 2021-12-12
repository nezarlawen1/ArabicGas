using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerState _state;
    public Player(PlayerState state)
    {
        _state = state;
    }
    // Start is called before the first frame update
    void Start()
    {
        _state = new StandingState();
    }

    // Update is called once per frame
    void Update()
    {
        _state.Action(this);
    }
    public PlayerState CurrentState
    {
        get { return _state; }
        set { _state = value; }
    }
    public IEnumerator HangTime()
    {     
        yield return new WaitForSeconds(1f);
    }
}
public abstract class PlayerState
{
    public abstract void Action(Player player);
}

public class StandingState : PlayerState
{
    public override void Action(Player player)
    {
        Debug.Log("Stand");
        Vector3 tempScale = player.gameObject.transform.localScale;
        tempScale.y = 1f;
        player.gameObject.transform.localScale = tempScale;
        Vector3 tempPos = player.gameObject.transform.position;
        tempPos.y = 0f;
        player.gameObject.transform.position = tempPos;
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftControl))
        {
            player.CurrentState = new DivingState();
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            player.CurrentState = new DuckingState();
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            player.CurrentState = new JumpingState();
        }
    }
}

public class DuckingState : PlayerState
{
    public override void Action(Player player)
    {
        Debug.Log("Duck");
        Vector3 tempScale = player.gameObject.transform.localScale;
        tempScale.y = 0.75f;
        player.gameObject.transform.localScale = tempScale;
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            player.CurrentState = new StandingState();
        }
    }
}
public class JumpingState : PlayerState
{
    public override void Action(Player player)
    {
        Debug.Log("Jump");
        Vector3 tempPos = player.gameObject.transform.position;
        tempPos.y = 1f;
        player.gameObject.transform.position = tempPos;
        player.StartCoroutine(player.HangTime());
        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.CurrentState = new StandingState();
        }
    }
}
public class DivingState : PlayerState
{
    public override void Action(Player player)
    {
        Debug.Log("Dive");
        player.StartCoroutine(player.HangTime());
        player.CurrentState = new StandingState();
    }
}
