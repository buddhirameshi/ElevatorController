# Elevator Controller System

This project simulates a smart elevator control system that efficiently handles both inner requests (from passengers inside the elevator) and outer requests (from people waiting on different floors). The system optimizes elevator movement to minimize travel time while maintaining proper elevator operation protocols.

## System Specifications

- **Floor Range**: This is a 10-storey building from floor number 1 (level 1 or ground floor, both refer to the same floor) to 10 (top floor)
- **Elevator States**: Idle, Going Up, Going Down
- **Request Formats**: 
- **Outer Requests**: `[floor#direction]` (e.g., `[3#u],[5#d]` for floor 3 going up, floor 5 going down)
- **Inner Requests**: Comma-separated floor numbers (e.g., `3,8,7,5`)


### Prerequisites

- **.NET 9 SDK** or later
- **Visual Studio 2022** (17.8+) or **JetBrains Rider** or **Visual Studio Code**
- **Git** for version control

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/buddhirameshi/ElevatorController
   cd ElevatorController
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build the solution:**
   ```bash
   dotnet build
   ```

4. **Run the application:**
   ```bash
   dotnet run --project ElevatorController.ConsoleApp
   ```

5. **Run tests:**
   ```bash
   dotnet test
   ```

## 📖 User Guide

### Starting the Simulation
The elevator initializes at floor 1 in an idle state and provides interactive prompts.

### Making Outer Requests (Pickup Requests)
Request pickup from multiple floors with directions:
```
Enter: [3#u],[5#d],[8#u]
- Floor 3, going up
- Floor 5, going down  
- Floor 8, going up
```

### Making Inner Requests (Destination Floors)
Once inside, specify destination floors:
```
Enter: 3,8,7,5
```

### Sample Interactive Session
```
Elevator Simulation Started.
Hello, I'm the elevator controller
**********************
The lift is idle and currently at floor number 1
There are only 10 floors. The highest floor is 10, and the lowest floor is 1

Enter requests: [3#u],[5#d]
**********************
The lift stops at floor number 3.
The door is open. Welcome aboard
Floor = 3. Now that you are in, please enter destination floors: 8,5,10
The door is closed. Let's Go
```

## 📝 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---
