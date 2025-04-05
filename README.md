# Flight Follower
This project is an experimental Mixed Reality game where players use a VR controller to draw a virtual path, and a small airplane follows this path continuously in a loop. The game leverages the **Meta Mixed Reality Utility Kit** to interact with the user's real-world environment. Detected objects in the environment are used to create colliders, adding an immersive layer of interaction.

## Demo
![Demo](Demo.gif)

## The Experience
- **Environment Scanning**: When the game starts, it requests permission to scan the user's environment to detect objects. These objects are used to create colliders in the scene.
- **Path Drawing**: Players use a VR controller to draw a virtual path in the air.
- **Airplane Movement**: A small airplane follows the drawn path in a continuous loop.
- **Collision Effects**: If the airplane collides with a detected object, a visual explosion effect (VFX) is triggered.

## Getting Started

### Prerequisites
- Unity (recommended version: 2021.3 or later)
- Meta Mixed Reality Utility Kit
- Compatible VR hardware (e.g., Meta Quest 2 or similar)

### Setup Instructions
1. **Clone the Repository**:
    ```bash
    git clone https://github.com/YourUsername/FlightFollower.git
    cd FlightFollower
    ```
2. **Open in Unity**:
    - Launch Unity Hub and open the cloned project folder.
3. **Configure the Project**:
    - Ensure the Meta Mixed Reality Utility Kit is installed and properly configured.
4. **Run the Project**:
    - Connect your VR hardware.
    - Enter Play Mode in Unity to test the experience.

### Tips
- Ensure your VR hardware is properly set up and connected before running the project.
- Test in a well-lit environment for optimal object detection.

## Planned Features and Future Improvements
- Advanced collision effects with dynamic debris.
- Additional game mechanics, such as power-ups or challenges.
- Enhanced VFX and audio feedback for a more immersive experience.
- Improved interaction with real-world objects.
- UI improvements to guide the player during gameplay.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contributing
Contributions are welcome! If you have suggestions or improvements, feel free to open an issue or submit a pull request. Feedback is greatly appreciated.
