# Documentation review and manual checks

Baseline: `ed464ff5014872ee402cbd1c6b067aba0020c2b3`.

## Scope and evidence

README and validation notes only. No scripts, scenes, assets, settings, packages, or saved progress are changed.

Reviewed movement/combat and pickup paths, pointer controls, health/rescue/collectible counters, door teleportation, unlock flags, scene navigation, project metadata, and existing credit references. Checked local documentation links, five enabled scene paths, and patch whitespace.

Unity compilation, gameplay, animation events, menu wiring, and mobile builds were not tested. This is not a full-history security or asset-rights audit.

## Source observations

- `PlayerController` and `MovementJoystick` import `UnityEditor` outside an Editor folder. Check platform compilation before release; the imports remain unchanged.
- `PlayerController.Update()` gates movement with `!isDead || !isWin`. This expression remains true when only one flag is set. Verify that movement/combat stop as intended after death or completion.
- The generated movement action includes A/D. Keyboard jump/attack code is commented out, while pointer buttons call their respective methods. Test the actual on-screen and keyboard paths separately.
- Movement subscribes to `Land.Move.performed` without a matching cancellation subscription in the reviewed setup. Check whether releasing keyboard movement clears the stored direction and inspect mobile pointer-release wiring.
- Several scripts find objects by names such as MainCharacter, Canvas, Top, and Coin, then access fixed child indices. Confirm required objects, components, tags, and hierarchy positions in each level.
- Health begins at 100 and repeatedly invokes the end-screen path while depleted. Test repeated damage, healing, death, and restart behavior.
- Coin collection adds to the counter before its delayed destruction, and rescue counting likewise precedes delayed removal. Test repeated trigger events and audio failures for duplicate counting or stalled cleanup.
- `LevelController` reads a configured PlayerPrefs key every frame and logs it; `SetLevel.LevelSet()` sets the provided key. Inspect menu defaults and serialized calls before claiming a verified unlock sequence.
- Coin/rescue counters are scene state, while unlock flags use PlayerPrefs. Check end-screen timing and data retention instead of assuming a persistent inventory.
- Existing Credit scene references and bundled assets are retained; check asset permissions separately before public distribution.

## Manual checklist

1. Import in Unity 2022.3.29f1 and inspect compilation, references, tags, and build settings.
2. Start at MainMenu, exercise level selection/unlocking, and visit all three levels and Credit.
3. Test keyboard/pointer movement and release, ground jumps, melee/arrow animation events, enemy/projectile damage, and health pickups.
4. Check collectible/rescue counts, repeated contacts, doors, end triggers, summary displays, death, and replay.
5. Restart with test progress and verify level flags. Build and test the intended device platform and review included asset permissions.

Source concerns above remain unfixed in this documentation-only PR.
