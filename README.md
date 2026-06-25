# Shoot 'Em Up

A small 2D top-down space shooter built in **Unity 6**. Pilot a ship, dodge
enemies, and shoot them down. Originally built as a learning project (otus
game-development course).

![Unity 6](https://img.shields.io/badge/Unity-6000.5.1f1-000000?logo=unity&logoColor=white)

## Requirements

- **Unity 6000.5.1f1** (Unity 6) — open via Unity Hub.

## How to run

1. Clone the repo and add the project folder to Unity Hub.
2. Open it with Unity 6 (first open imports/generates the `Library/` cache).
3. Open the scene: **`Assets/Scenes/Game.unity`**.
4. Press **Play** ▶.

> If Play shows an empty scene, make sure `Assets/Scenes/Game.unity` is the open
> scene — Unity sometimes opens a fresh empty scene instead.

## Controls

| Action | Key |
| ------ | --- |
| Move left / right | **←** / **→** arrow keys |
| Fire | **Space** |

## Project structure

```
Assets/Scripts/
├── Bullets/        # Bullet, BulletConfig, BulletSystem, BulletUtils
├── Character/      # CharacterController (player fire + death)
├── Common/         # PhysicsLayer
├── Components/     # MoveComponent, HitPointsComponent, WeaponComponent, TeamComponent
├── Enemy/          # EnemyManager, EnemyPool, EnemyPositions, Agents (move/attack)
├── GameManager/    # GameManager (game lifecycle)
├── Input/          # InputManager (movement + fire)
└── Level/          # LevelBackground, LevelBounds
```

## Architecture notes

- `GameManager` drives the game lifecycle (start/finish).
- `InputManager` reads keyboard input and forwards movement to `MoveComponent`
  and fire requests to `CharacterController`.
- `BulletSystem` spawns and pools bullets for both the player and enemies.
- `EnemyManager` + `EnemyPool` spawn enemies at `EnemyPositions` and move/attack
  via the agent components.
- Movement uses `Rigidbody2D` velocity / `MovePosition`.

## License

All rights reserved (no license granted) unless stated otherwise.
