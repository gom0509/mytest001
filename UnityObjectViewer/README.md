# Unity Android 3D Object Viewer

This directory contains guidance and reusable scripts for building an Android 3D object viewer in Unity that supports touch-based rotation, zoom and panning.

## Getting started

1. **Create a new Unity project** using the 3D (URP or Built-in) template.
2. Copy the contents of the `Assets` folder from this repository into your project's `Assets` directory. The `TouchObjectController` script will appear under `Assets/Scripts`.
3. In Unity, create or select an empty GameObject (for example `ModelRoot`) that will act as the parent for the model you want to manipulate.
4. Drag your 3D model under `ModelRoot` so the entire object moves together when the parent is transformed.
5. With `ModelRoot` selected, click **Add Component** in the Inspector and choose **Scripts ▸ Touch Object Controller** (or search for `TouchObjectController`).
6. Assign a camera in the scene. The script automatically uses `Camera.main`, so ensure the camera you want to reference has the **Main Camera** tag.
7. Configure the public properties in the Inspector to match the desired feel:
   - **Rotation Speed**: How fast the model rotates when dragging with one finger.
   - **Zoom Speed**: How responsive the pinch gesture is.
   - **Min/Max Scale Multipliers**: Clamp the scale relative to the object's starting scale.
   - **Pan Speed**: Controls how far the object moves in world space during a two-finger pan.

### Applying the controller script in Unity

1. After copying the files, return to the Unity Editor. Unity will automatically compile `TouchObjectController.cs`.
2. In the **Project** window, confirm the script appears under `Assets/Scripts`. If it is missing, right-click the folder and choose **Reimport**.
3. Select the GameObject that should react to touches (for example `ModelRoot`).
4. In the **Inspector**, click **Add Component** → search for `TouchObjectController` → select it. The component now controls the object's rotation, zoom, and pan on Android builds.
5. Enter **Play Mode** or build to a device to test. Drag with one finger to rotate, pinch to zoom, and drag with two fingers to pan.

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

