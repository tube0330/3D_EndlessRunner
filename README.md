# 240924
1. Remy Movement 스크립트 작성
2. Animation 적용
3. Platform ObjectPooling

# 240926
1. ObjectPooling된 PlatformList들을 담을 List를 만들어 List에 추가

# 240927
1. 플랫폼마다 ObjectPooling하지 않고 Grid Snapping을 사용해 플랫폼을 미리 만들어둔 후 사용

# 240929
1. ObjectPooling에 맞게 GameManager 스크립트에서 플랫폼들을 Instantiate하지 않고  ObjectPooling 스크립트의 함수를 불러오도록 수정
2. 플랫폼마다 MovePlatform 스크립트를 추가해 Player는 가만히 있고 플랫폼이 움직이도록 설정
3. ObjectPooling 스크립트에서 코드를 중복되게 사용하지 않고 함수 하나로 모든 종류의 플랫폼 Pool을 생성할 수 있도록 통합
4. 전체 플랫폼을 반환하는 로직에서 비활성화된 플랫폼만 랜덤으로 반환하는 로직으로 수정

# 240930
1. ObjectPooling script중 GetPlatform() 함수에서 inactivePlatforms 리스트에 비활성화된 플랫폼을 추가할 때 매번 해당 리스트를 초기화하지 않으면 이전 호출 때 추가된 플랫폼이 계속 누적되어 이미 활성화된 플랫폼이 다시 선택되는 문제가 발생. 이것을 막기 위해 GetPlatform() 함수를 호출할 때마다 inactivePlatforms 리스트를 초기화하는 로직 추가
2. random obstacle

# 241001
1. ObstacleDetect 스크립트를 만들고 Player에게 추가해 Player가 장애물을 감지
2. 장애물을 더 정확하게 감지할 수 있도록 Jump, Slide 할 때 Player의 Collider 위치와 높이를 조절
3. Random.insideUnitSphere를 사용해 Camera Shake

# 241002
1. 무한점프 막기

# 241008
1. 플랫폼 겹치는 문제 수정
2. 달린 기록 표시
- 점수가 너무 빠르게 증가하는 문제를 해결하기 위해, 소수점 이하 값을 일정하게 축적해서 일정 값 이상이 되면 정수로 변환
3. OnCollisionExit()대신 IEnumerator Jump()로 isGroud true, false 관리
4. Platform으로 Raycast를 아래로 쏴 Platform이 없으면 Obstacle setActive(false)
너무 가까우면 감지를 하지 못해 위치를 ray의 origin 위치를 약간 올림
5. Obstacle이 Platform을 감지하지 못해 setActive(false)된 자식 object가 Pool로 들어가 다시 켜질 때 setActive(true)가 되지 않는 문제를 해결하기 위해 GameManager 스크립트의 ActiveObstacle()에서 for문을 사용해 Obstacle의 childCount를 SetActive(true)로 설정

# 241014
1. 플랫폼 움직이는 속도를 3단계로 나누어 난이도 조절
- 플랫폼이 움직이는 속도에 맞춰 점수도 같은 비율로 증가
- 속도가 바뀔 때 UI를 띄워 속도가 증가했다는 것을 보여줌

# 241015
1. Player의 위치에 따른 MainCamera Position 설정
2. Exit Button 생성

처음에는 장애물 조금나ㄱ오고 나중가면갈수록 빈번하게 (wait 
체력 닳거나 땅에 떨어지면 playerDie
Replay Button 만들기
Exit Button 만들기
score 기록 + 랭킹

LOD 해야됨
아래로 raycast쏴서 없으면 점프 못하게(바닥에 떨어졌을 때 점프하지 못하도록 막기 위해서)