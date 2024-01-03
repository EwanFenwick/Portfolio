using Portfolio.Quests;
using UnityEngine;
using Zenject;

public class QuestInstaller : MonoInstaller {
    public override void InstallBindings() {
        Container.BindFactory<QuestInfo, GameObject, Quest, QuestFactory>().FromFactory<CustomQuestFactory>().NonLazy();

        Container.BindFactory<QuestStepInfo, GameObject, QuestStep, QuestStepFactory>().FromFactory<CustomQuestStepFactory>().NonLazy();

        Container.Bind<QuestManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
}