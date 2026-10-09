# Save the Forest

A Unity 2D platformer prototype with three level scenes, rescue objectives, collectible cleanup, health pickups, and enemy encounters. The player moves and jumps through each level, collects items, rescues characters, and reaches an endpoint.

## Features in the source

- Rigidbody2D movement, ground detection, jumping, animated melee attacks, and an arrow-shooting path.
- Pointer-based movement, jump, and attack controls; generated Input System movement bindings include **A/D**.
- Health starts at **100**. The player-damage path subtracts **10**, health pickups add **10**, and health is capped at 100.
- A rescue counter, collectible counter, and end-of-level summary. The collectible implementation uses coin names internally, while the summary labels them **Trash Collected**.
- Triggered door teleportation and end-of-level/death panels.
- Level-unlock flags stored through Unity `PlayerPrefs`.

Counters for collectibles and rescues are maintained by scene components; they are not documented as a persistent inventory. Gameplay flow and mobile controls still need runtime verification. See [validation notes](docs/VALIDATION.md).

## Open the project

1. Clone the complete repository and open its root through Unity Hub.
2. Use **Unity 2022.3.29f1**, recorded in [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt).
3. Allow asset import and [package resolution](Packages/manifest.json). The manifest includes Input System 1.7.0, TextMesh Pro 3.0.6, uGUI 1.0.0, and the mobile/adaptive-performance packages.
4. Open [MainMenu](Assets/Scenes/MainMenu.unity) and check level selection and navigation using test progress.

[EditorBuildSettings.asset](ProjectSettings/EditorBuildSettings.asset) enables `MainMenu`, `Level1`, `Level2`, `Level3`, and `Credit`, in that order. `SampleScene` is disabled. A configured scene list does not establish that all menu buttons and unlock flows work correctly.

Keyboard movement is present in the generated action bindings, but keyboard jump/attack handlers in the player source are commented out. Use the on-screen controls for those actions and verify their scene wiring before claiming a complete keyboard control scheme.

## Source guide

| Area | Source |
| --- | --- |
| Movement, combat, pickups, rescue, and end triggers | [PlayerController.cs](Assets/PlayerController.cs) |
| Pointer attack and jump buttons | [AttackButton.cs](Assets/AttackButton.cs), [JumpButtonController.cs](Assets/JumpButtonController.cs) |
| Collectible and summary display | [CoinsTrigger.cs](Assets/CoinsTrigger.cs), [ScoreManager.cs](Assets/ScoreManager.cs), [GetCollectedPoint.cs](Assets/GetCollectedPoint.cs) |
| Rescue and health counters | [HostageController.cs](Assets/HostageController.cs), [HealthController.cs](Assets/HealthController.cs) |
| Door teleportation | [DoorController.cs](Assets/DoorController.cs) |
| Level flags and scene navigation | [LevelController.cs](Assets/LevelController.cs), [SetLevel.cs](Assets/SetLevel.cs), [SceneController.cs](Assets/SceneController.cs) |

## Credits and status

The [Credit scene](Assets/Scenes/Credit.unity) includes references to Craftpix.net and Pixabay.com. Preserve the project's existing credits and applicable asset terms; this documentation does not establish redistribution rights for every bundled asset or add a repository-wide license.

This review changed documentation only. Unity compilation, Play Mode, animation-event wiring, and device builds were not tested.
