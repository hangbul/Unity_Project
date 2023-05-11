using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICommand
{
    void Execute(Transform tr);
    void Undo(Transform tr);

}
public class MoveRight : ICommand
{
    public void Execute(Transform tr)
    {
        tr.Translate(Vector3.right);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.left);
    }
}
public class MoveLeft : ICommand
{
    public void Execute(Transform tr)
    {
        tr.Translate(Vector3.left);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.right);
    }
}
public class MoveFoward : ICommand
{
    public void Execute(Transform tr)
    {
        tr.Translate(Vector3.forward);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.back);
    }
}
public class MoveBack : ICommand
{
    public void Execute(Transform tr)
    {
        tr.Translate(Vector3.back);
    }
    public void Undo(Transform tr)
    {
        tr.Translate(Vector3.forward);
    }
}
public class StudyCommandPattern : MonoBehaviour
{
    ICommand Akey;
    ICommand Skey;
    ICommand Dkey;
    ICommand Wkey;

    Stack<ICommand> commandList;

    private void Start()
    {
        commandList = new Stack<ICommand>();
        Akey = new MoveLeft();
        Skey = new MoveBack();
        Dkey = new MoveRight();
        Wkey = new MoveFoward();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Akey.Execute(transform);
            commandList.Push(Akey);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Skey.Execute(transform);
            commandList.Push(Skey);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dkey.Execute(transform);
            commandList.Push(Dkey);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Wkey.Execute(transform);
            commandList.Push(Wkey);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (commandList.Count > 0) commandList.Pop().Undo(transform);
        }

    }
    }
