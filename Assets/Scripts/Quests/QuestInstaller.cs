using Portfolio.Quests;
using UnityEngine;
using Zenject;
using static Portfolio.Quests.QuestEnums;

public class QuestInstaller : MonoInstaller {
    public override void InstallBindings() {
        Container.BindFactory<QuestInfo, GameObject, Quest, QuestFactory>().FromFactory<CustomQuestFactory>().NonLazy();

        Container.BindFactory<QuestStepType, QuestInfoArgs, GameObject, QuestStep, QuestStepFactory>().FromFactory<CustomQuestStepFactory>().NonLazy();

        Container.Bind<QuestManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
}