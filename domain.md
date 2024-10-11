# Notes for domain model

> NOTE: viewing this file might be better if rendered

## Entities with attributes

The methods can be included in the domain model if it is necessary for the
clarity of the model (see assignment details).

- Character
  - properties:
    - -name: string
    - -position: (int, int)
    - -orientation: string
  - methods:
    - +performCommand(command: Command): void
- Command (abstract class)
  - subclasses:
    - Turn
      - +direction: string
    - Move
      - properties:
        - +steps: int
    - Repeat
      - properties:
        - +commands: \[Command\]
- Program
  - properties:
    - -name: string
    - -commands: \[Command\]
    - +Examples: \[Program\] (static property)
  - methods:
    - +Execute(): void
    - +CalculateMetrics(): void
    - +Import(file: string): void
