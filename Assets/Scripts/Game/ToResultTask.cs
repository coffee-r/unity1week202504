using System;
using LogicToolkit;

[System.Serializable]
public class ToResultTask : TaskComponent
{
    public GameMediator gameMediator;

    // OnActivated is called when activated.
    protected override async void OnActivated()
    {
        var sceneContext = ServiceLocator.Instance.Resolve<SceneContext>();
        sceneContext.Score = gameMediator.Time;

        await ServiceLocator.Instance.Resolve<SceneRouter>().NavigateToAsync(
            "Scenes/Result",
            ServiceLocator.Instance.Resolve<IScreenTransitionEffect>("Default")
        );
    }

    // OnExecute is called when it is executed.
    protected override TaskStatus OnExecute()
    {
        // Return Running, Success, or Failure depending on the task execution status.
        return TaskStatus.Running;
    }

    // OnDeactivated is called when it is deactivated.
    protected override void OnDeactivated()
    {
        
    }
}
