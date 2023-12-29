# Portfolio
Hi! My name is Ewan Fenwick. This project is a resource to collect all of my programming knowledge and display it to those interested.

# Running the project
This project is made with Unity2022.3.14f1.

# Code Highlights
To save scouring through the project for examples of my code, I suggest these classes as highlights:
* The [PopupController](Assets/Scripts/Popups/PopupController/PopupController.cs) and its [PopupControllerView](Assets/Scripts/Popups/PopupController/PopupControllerView.cs).
* The lightweight and asynchronous [TweenController](Assets/Scripts/Tweening/TweenController.cs) that allows for simple tween animations for a variety of components following an ```AnimationCurve```.
* The simple [EventBus](Assets/Scripts/EventBus/EventBus.cs) system and the [EventBus SubscriberComponents](Assets/Scripts/EventBus/SubscriberComponents) that create compatibility with the editor and integrate UnityEvents.

# Coding Conventions
When beginning this project I decided upon the following patterns and conventions:
* A Model-View-Presenter architectural pattern to improve seperation of concerns.
* Dependency Injection to create a more loosely coupled system.
* The "One True Brace Style" of bracket placement, which allows for new lines of code to be easily inserted anywhere.
* Class-Scope variables are  a denoted with ```_```.
* Trailing Commas are used for simpler and cleaner merges.

<details>
<summary>Regions</summary>
  
  For organisational purposes I have divided my code into regions that group similar areas. Use of regions in this way is mostly a personal preference, as it allows me to more easily focus on code that is relevent to my current task.
  
  ```
  #region Consts

  #region Editor Variables

  #region Variables

  #region Properties

  Constructor

  #region Lifecycle

  #region Public Methods

  #region Protected Methods

  #region Private Methods
  ```

</details>

# External Libraries/Tools Used
* [Extenject](https://github.com/Mathijs-Bakker/Extenject) for dependency injection
* [UniTask](https://github.com/Cysharp/UniTask) for async development
* [UniRx](https://github.com/neuecc/UniRx) for reactive extentions
* [SceneLoader](https://github.com/mygamedevtools/scene-loader/tree/main) by Joao Borks
* Ink for dialogue
* The third person controller was built following [this tutorial](https://blog.logrocket.com/building-third-person-controller-unity-new-input-system/) by Marian Pekár / LogRocket
