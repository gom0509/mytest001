# 유니티 안드로이드 3D 오브젝트 뷰어

이 디렉터리는 안드로이드용 3D 오브젝트 뷰어를 구현할 때 사용할 수 있는 `TouchObjectController` 스크립트와 설정 방법을 제공합니다. 멀티 터치 제스처로 모델을 회전, 확대/축소, 이동할 수 있습니다.

## 시작하기

1. **새 Unity 프로젝트 생성**: 3D(URP 또는 Built-in) 템플릿으로 프로젝트를 생성합니다.
2. 이 저장소의 `Assets` 폴더를 프로젝트의 `Assets` 디렉터리에 복사합니다. `TouchObjectController` 스크립트는 `Assets/Scripts` 경로에 나타납니다.
3. 씬에서 모델을 담을 빈 게임 오브젝트(예: `ModelRoot`)를 만들거나 선택합니다.
4. 조작할 3D 모델을 `ModelRoot` 하위로 옮겨 부모 오브젝트와 함께 이동하도록 구성합니다.
5. `ModelRoot`를 선택한 상태에서 인스펙터의 **Add Component** 버튼을 눌러 **Scripts ▸ Touch Object Controller**(또는 검색) 컴포넌트를 추가합니다.
6. 장면에 사용할 카메라를 지정합니다. 스크립트는 `Camera.main`을 기본으로 사용하므로, 대상 카메라에 **Main Camera** 태그가 지정되어 있는지 확인합니다.
7. 인스펙터에서 공개 변수 값을 조정하여 제스처 감도를 원하는 대로 맞춥니다.
   - **회전 속도(Rotation Speed)**: 한 손가락 드래그 시 회전 반응 속도.
   - **확대/축소 속도(Zoom Speed)**: 핀치 제스처 시 반응 속도.
   - **최소/최대 배율(Min/Max Scale Multipliers)**: 시작 크기를 기준으로 허용되는 배율 범위.
   - **이동 속도(Pan Speed)**: 두 손가락 드래그 시 이동 속도.

## 유니티에서 스크립트 적용하기

1. 파일을 복사한 뒤 Unity 에디터로 돌아오면 `TouchObjectController.cs`가 자동으로 컴파일됩니다.
2. **Project** 창에서 스크립트가 `Assets/Scripts`에 존재하는지 확인합니다. 보이지 않으면 해당 폴더를 우클릭하여 **Reimport**를 실행합니다.
3. 제스처에 반응할 게임 오브젝트(`ModelRoot`)를 선택합니다.
4. **Inspector**에서 **Add Component** → `TouchObjectController`를 검색 후 추가합니다. 이제 이 컴포넌트가 회전, 확대/축소, 이동을 모두 제어합니다.
5. **Play Mode**로 실행하거나 실제 기기에 빌드하여 제스처 동작을 테스트합니다. 한 손가락 드래그로 회전하고, 핀치로 확대/축소하며, 두 손가락 드래그로 이동할 수 있습니다.

## 안드로이드 빌드 설정

1. Unity Hub에서 **Android Build Support**(SDK & NDK 포함)를 설치합니다.
2. **File ▸ Build Settings**에서 플랫폼을 **Android**로 전환하고 **Switch Platform**을 클릭합니다.
3. **Player Settings ▸ Other Settings**에서 다음을 확인합니다.
   - `Auto Graphics API`를 활성화하거나 OpenGLES3 / Vulkan을 수동으로 선택합니다.
   - `Scripting Backend`를 배포용으로 IL2CPP로 설정합니다.
   - `Target Architectures`에 ARM64를 포함합니다.
4. 저사양 기기에서 이동 시 끊김이 있다면 `Multithreaded Rendering`을 비활성화해 볼 수 있습니다.
5. 패키지 이름(예: `com.example.objectviewer`)과 서명용 키스토어를 설정합니다.
6. **Build** 또는 **Build And Run**을 눌러 연결된 안드로이드 기기에 배포합니다.

## 제스처 동작 요약

- **한 손가락 드래그**: 글로벌 Y축과 카메라 기준 X축을 따라 모델을 회전합니다.
- **두 손가락 핀치**: 오브젝트를 균일하게 확대/축소하며 지정한 최소/최대 배율 사이로 제한합니다.
- **두 손가락 드래그**: 카메라의 오른쪽/위쪽 벡터를 기준으로 오브젝트를 이동합니다.

## 팁

- 이동 범위를 제한하고 싶다면 콜라이더나 UI 영역을 배치해 사용자의 이동을 제약하세요.
- 고정된 카메라 연출이 필요하다면 다른 카메라 제어 스크립트와 조합해 사용할 수 있습니다.
- Unity **Device Simulator** 패키지를 사용하면 에디터에서 기본적인 터치 제스처를 미리 확인할 수 있습니다.
