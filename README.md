# AntonWessel.GameFramework

Simple .NET 10 library for a turn-based 2D game assignment.

The package contains the reusable `GameFramework` library.

The repository also includes a small console demo application.

Included features:

- world, creatures, and world objects
- attack and defence items
- XML configuration loading
- logging with `MyLogger`

Design patterns shown in the framework:

- `Creature` uses Template Method through its shared `Hit()` flow and overridable damage-adjustment hook.
- `Creature` also supports Strategy-based damage calculation.
- `Creature` supports Observer notifications for hit and death events.
- `AttackItem` includes Decorator and Composite implementations.
