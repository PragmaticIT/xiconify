set nuget="../.nuget/nuget.exe"

mkdir nugets

cd xamarin-iconify-common
%nuget% pack -prop Configuration=Release -OutputDirectory ../nugets/
cd ../xamarin-iconify
%nuget% pack -prop Configuration=Release -OutputDirectory ../nugets/
cd ../xamarin-iconify-entypo
%nuget% pack -prop Configuration=Release -OutputDirectory ../nugets/
cd ../xamarin-iconify-fontawesome
%nuget% pack -prop Configuration=Release -OutputDirectory ../nugets/
cd ../xamarin-iconify-ionicons
%nuget% pack -prop Configuration=Release -OutputDirectory ../nugets/
cd ../xamarin-iconify-material
%nuget% pack -prop Configuration=Release -OutputDirectory ../nugets/
cd ../xamarin-iconify-material-community
%nuget% pack -prop Configuration=Release -OutputDirectory ../nugets/
cd ../xamarin-iconify-meteocons
%nuget% pack -prop Configuration=Release -OutputDirectory ../nugets/
cd ../xamarin-iconify-simplelineicons
%nuget% pack -prop Configuration=Release -OutputDirectory ../nugets/
cd ../xamarin-iconify-typicons
%nuget% pack -prop Configuration=Release -OutputDirectory ../nugets/
cd ../xamarin-iconify-weathericons
%nuget% pack -prop Configuration=Release -OutputDirectory ../nugets/

cd ../




