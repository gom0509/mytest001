# Unity Android 3D Object Viewer

This directory contains guidance and reusable scripts for building an Android 3D object viewer in Unity that supports touch-based rotation, zoom and panning.

## Getting started

1. **Create a new Unity project** using the 3D (URP or Built-in) template.
2. Copy the contents of the `Assets` folder from this repository into your project's `Assets` directory. The `TouchObjectController` script will appear under `Assets/Scripts`.
3. Import or create the 3D model you want to display. Place it in the scene as the child of an empty GameObject (e.g., `ModelRoot`).
4. Attach the `TouchObjectController` component to the parent GameObject that should respond to gestures (`ModelRoot`).
5. Assign a camera in the scene. The script uses `Camera.main` to determine rotation axes and pan direction.
6. Configure the public properties in the Inspector to match the desired feel:
   - **Rotation Speed**: How fast the model rotates when dragging with one finger.
   - **Zoom Speed**: How responsive the pinch gesture is.
   - **Min/Max Scale Multipliers**: Clamp the scale relative to the object's starting scale.
   - **Pan Speed**: Controls how far the object moves in world space during a two-finger pan.

## Android build settings

1. Install the **Android Build Support** (SDK & NDK Tools) using the Unity Hub.
2. Open **File ▸ Build Settings**, switch the target platform to **Android**, and click **Switch Platform**.
3. In **Player Settings ▸ Other Settings**, ensure the following:
   - `Auto Graphics API` is enabled (or manually select OpenGLES3 / Vulkan).
   - `Scripting Backend` set to IL2CPP for release builds.
   - `Target Architectures` include ARM64.
4. Optionally disable `Multithreaded Rendering` on low-end devices if you experience artifacts when panning.
5. Set your package name (e.g., `com.example.objectviewer`) and keystore for signing.
6. Click **Build** or **Build And Run** to deploy to a connected Android device.

## Gesture behavior

- **Single-finger drag** rotates the model around the global Y-axis and relative camera X-axis.
- **Two-finger pinch** scales the model uniformly, clamped between configurable min/max multipliers.
- **Two-finger pan** moves the model in world space along the camera's right and up vectors, allowing the user to reposition the object.

## Tips

- Place colliders or UI boundaries in the scene if you need to restrict how far the model can move.
- Combine this script with a stationary camera or additional camera orbit controls for alternative viewpoints.
- Use the Unity **Device Simulator** package to preview gestures within the editor.

