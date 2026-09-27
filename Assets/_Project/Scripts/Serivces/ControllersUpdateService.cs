using System;
using System.Collections.Generic;

public class ControllersUpdateService
{
    private readonly List<ControllerToRemoveReason> _controllers = new List<ControllerToRemoveReason>();

    public void Add(Controller controller, Func<bool> removeReason)
    {
        _controllers.Add(new ControllerToRemoveReason(controller, removeReason));
    }

    public void Update(float deltaTime)
    {
        _controllers.RemoveAll(element => element.RemoveReason.Invoke());

        foreach (ControllerToRemoveReason element in _controllers)
            element.Controller.Update(deltaTime);
    }

    private class ControllerToRemoveReason
    {
        public ControllerToRemoveReason(Controller controller, Func<bool> removeReason)
        {
            Controller = controller;
            RemoveReason = removeReason;
        }

        public Controller Controller { get; }

        public Func<bool> RemoveReason { get; }
    }
}