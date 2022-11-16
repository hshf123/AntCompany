## MobileGame

포트폴리오 용도로 제작된 게임입니다!

* 목록
  * [전체적인 프레임 워크](https://github.com/hshf123/MobileGame/edit/main/README.md#%EC%A0%84%EC%B2%B4%EC%A0%81%EC%9D%B8-%ED%94%84%EB%A0%88%EC%9E%84-%EC%9B%8C%ED%81%AC)
  * [데이터 구조](https://github.com/hshf123/MobileGame/edit/main/README.md#%EB%8D%B0%EC%9D%B4%ED%84%B0-%EA%B5%AC%EC%A1%B0)
  * [게임 진행 방식](https://github.com/hshf123/MobileGame/edit/main/README.md#%EA%B2%8C%EC%9E%84-%EC%A7%84%ED%96%89-%EB%B0%A9%EC%8B%9D)
  * [기타](https://github.com/hshf123/MobileGame/edit/main/README.md#%EA%B8%B0%ED%83%80)

## 전체적인 프레임 워크
기반이 되는 프레임 워크는 [Rookiss님의 인프런 강의 C#과 유니티로 만드는 mmorpg part3](https://www.inflearn.com/course/mmorpg-%EC%9C%A0%EB%8B%88%ED%8B%B0/dashboard)를 토대로 만들었습니다.
간단하게만 설명하자면 Singleton으로 만들어진 Manager라는 객체가 있고 전역으로 접근해야하는 모든 것은 Manager를 통해서 이루어집니다. 유니티에서 제공하는 함수들 또한 Manager로 매핑해서 만들어 더 사용하기
편하게끔 되어 있습니다. 추가로 컨텐츠적인 부분 또한 별개로 만들어서 사용했는데 인벤토리, 스킬, 데이터(플레이어, 몬스터 등등의 정보)를 담당하는 Manager를 추가하여 작성하였습니다.
## 데이터 구조
데이터 같은 경우 Json파일로 이루어져있으며, 몬스터, 플레이어, 화살, 장비, 스킬을 담당하는 파일이 있고, 추가로 생성되어 저장되는 Save파일에는 플레이어 정보와 착용한 장비, 스킬 정보가 저장되게 됩니다.
플레이어 정보가 변경되거나 새롭게 업데이트 되어야하는 경우 업데이트 정보를 Save파일에 저장하고 이 Save파일을 다시 읽어오는 방식으로 동작하기 때문에 Save가 자주 일어나는 편입니다. 최대한 메모리에
들고있다가 바로 적용하는 것을 피하기위해 이런식으로 저장되게끔 하였습니다.
## 게임 진행 방식
게임 진행 방식은 간단합니다. 타이틀 화면에서 새로하기, 이어하기를 선택할 수 있고, 이어하기를 눌렀을 때 Save파일이 없으면 새로 시작하게 됩니다. 레벨별로 착용할 수 있는 장비와 스킬 개수가 다르기 때문에,
레벨을 올리고 더 높은 능력치를 갖고있는 장비와 스킬을 착용하고, 각 스테이지를 클리어하면 됩니다.
## 기타
이 프로젝트를 내려 받으셔도 데이터 파일은 있지만, 이미지와 사운드 등의 리소스는 올려놓지 않아서 실행이 되지 않으실 겁니다. 유니티 무료 에셋과, [Rookiss님의 실전 게임 코드 리뷰](https://www.inflearn.com/course/%EC%8B%A4%EC%A0%84%EA%B2%8C%EC%9E%84-%EC%BD%94%EB%93%9C%EB%A6%AC%EB%B7%B0-%EC%9C%A0%EB%8B%88%ED%8B%B0-%ED%81%B4%EB%A6%AC%EC%BB%A4/dashboard)
의 리소스를 사용하여 게임을 제작했지만 아무래도 직접 만든 리소스들이 아니다 보니 무료라고 해도 저작권 관련해서는 아무것도 모르는 터라... 그냥 아예 제외하는 것을 선택했습니다.

---
**게임 실행 이미지**  
<img src="https://user-images.githubusercontent.com/66163506/202067074-aa9725c5-3a75-4081-87f3-7d0477135587.png" width="284" height="623"/>
<img src="https://user-images.githubusercontent.com/66163506/202067079-6f2ddb76-999d-43d4-84e6-6057490cfcb9.png" width="284" height="623"/>
<img src="https://user-images.githubusercontent.com/66163506/202067081-61e9a79e-e083-474a-9e93-065b3b1fbe9c.png" width="284" height="623"/>
<img src="https://user-images.githubusercontent.com/66163506/202067084-52918a0b-2064-4663-af8f-6b3bcf858a02.png" width="284" height="623"/>
<img src="https://user-images.githubusercontent.com/66163506/202067087-fb8824bf-d088-4037-b5ed-5cfb5cf4c72a.png" width="284" height="623"/>
<img src="https://user-images.githubusercontent.com/66163506/202067089-eedf7160-33c9-4193-acd8-590840b8369f.png" width="284" height="623"/>
---

## TODO
```
스킬과 장비창이 다 찼을 때 새로운 스킬과 장비를 착용하게 되면 첫 번째 슬롯에 착용되게끔 되어있는데 한 번 선택한 장비는 해제할 수 없기 때문에 이 부분을 수정할 생각입니다.
추가로 리소스의 경우 정리해서 문제가 되지 않는 선에서 올려 게임 실행이 가능 할 수 있도록 할 계획이며 광고 기능도 추가할 생각입니다.
```

