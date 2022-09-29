# UnityCSVtoScriptableObject

Now you can read csv easly and can make scirptable objects automatically

## Test

* Datas for test
  
  | IndexInt | NameString | LengthFloat      | RandomVector3 | RandomVector2 |
  |:--------:|:----------:|:----------------:|:-------------:|:-------------:|
  | 0        | 김가나        | 150.5            | (1,2,3)       | 1.1,1.2       |
  | 1        | 이다라        | tall : 180.35 cm | 1,1,1         | (-5  ,   6)   |
  | 02       | 박馬바        | BIG -160.12345   | 2\|3\|-4      | 1000\|5000    |
  | 03       | SaAh Choi  | .09              | ,1,           | (24\|25)      |
  | 4        | 정자차        | 1m               | 0.1, -0.5, .1 | 1             |

* Result    
  
  | ![image](https://user-images.githubusercontent.com/71427168/192750912-29c038b2-1c34-43e8-95a2-b88608a0039f.png) | ![image](https://user-images.githubusercontent.com/71427168/192751005-a7e46ba7-0562-4366-b92d-58b2de993652.png)                                            |
  | --------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
  | ![image](https://user-images.githubusercontent.com/71427168/192918239-254f96ab-61a1-42e6-856a-879403348937.png) | <img title="" src="https://user-images.githubusercontent.com/71427168/192751066-0a0d5d9e-a301-432f-aa19-23d4ab34f241.png" alt="image" data-align="inline"> |
  | ![image](https://user-images.githubusercontent.com/71427168/192918156-8312beb5-d7a6-4e1d-894d-58a94ecf004a.png) |                                                                                                                                                            |

## How to use it

* After install package [**Tools**] -> [**DataTable**] -> [**New Table**] menu will be created.
  <img title="" src="https://user-images.githubusercontent.com/71427168/192751865-d075da2f-a59e-4453-8153-6512fe7ed480.png" alt="image" width="1014">  

* Select [**NewTable**] then **Define New Datatable** tab will appear.
  <img title="" src="https://user-images.githubusercontent.com/71427168/192914567-76835e08-ab9e-4771-9ceb-0bdc3d943be9.png" alt="image" width="820">  

* Click [**Browse..**] button to find data sheet(.csv)
  <img title="" src="https://user-images.githubusercontent.com/71427168/192914629-64f5c7cd-149b-40ac-ad94-72a123c75bec.png" alt="image" width="862">  

* Then select correct type of each field.
  <img title="" src="https://user-images.githubusercontent.com/71427168/192915248-d24326a9-bdb3-4a07-8e5c-627955b061ba.png" alt="image" width="757">  

* If all type of fields are correct, Click [**Gererate Scripts**] button for generate scripts.
  <img title="" src="https://user-images.githubusercontent.com/71427168/192916029-ace9c3a1-ad4e-4704-880f-2255e50a1e14.png" alt="image" width="783">  <img title="" src="https://user-images.githubusercontent.com/71427168/192916257-65d23480-a6ab-4c66-a511-4f43508d6d65.png" alt="image" width="802">

* After compile scripts **[Create] -> [DataTable] -> [{*dataFileName*}Data]** menu created.<img title="" src="https://user-images.githubusercontent.com/71427168/192916842-f720bff6-c7a3-4481-9881-559381c399d9.png" alt="image" width="751">

* Select above menu then "**New{*dataFileName*}Data**" will be created.
  
  <img title="" src="https://user-images.githubusercontent.com/71427168/192917278-24721f75-9566-4506-b21e-68f246c2d450.png" alt="image" width="674">

* Click **[Import]** button at created scriptable object's Inspector.
  
  <img src="https://user-images.githubusercontent.com/71427168/192917486-ed6e57c0-4a67-4a27-8f48-eddb96b5fd75.png" title="" alt="image" width="912">

* Then **Row List** will be filled and **child scriptable objects** will be created.<img src="https://user-images.githubusercontent.com/71427168/192917731-3e7fb7e0-9513-422c-be27-fa1fa8917138.png" title="" alt="image" width="883">



* Done!
  You can get sample scene, sample data at samples folder in package.