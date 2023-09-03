function initMap() {
    const warsaw = { lat: 52.2297, lng: 21.0122 }; // Coordinates for Warsaw

    const map1 = new google.maps.Map(document.getElementById('map1'), {
        center: warsaw,
        zoom: 10
    });

    const map2 = new google.maps.Map(document.getElementById('map2'), {
        center: warsaw,
        zoom: 10
    });

    const input1 = document.getElementById('search-box1');
    const searchBox1 = new google.maps.places.SearchBox(input1);

    const input2 = document.getElementById('search-box2');
    const searchBox2 = new google.maps.places.SearchBox(input2);

    map1.addListener('bounds_changed', function () {
        searchBox1.setBounds(map1.getBounds());
    });

    map2.addListener('bounds_changed', function () {
        searchBox2.setBounds(map2.getBounds());
    });

    let markers1 = [];
    let markers2 = [];

    searchBox1.addListener('places_changed', function () {
        const places = searchBox1.getPlaces();
        handlePlacesChanged(map1, markers1, places, "map1");
    });

    searchBox2.addListener('places_changed', function () {
        const places = searchBox2.getPlaces();
        handlePlacesChanged(map2, markers2, places, "map2");
    });

    function handlePlacesChanged(map, markers, places, mapId) {
        if (places.length === 0) {
            return;
        }

        markers.forEach(function (marker) {
            marker.setMap(null);
        });
        markers = [];

        const bounds = new google.maps.LatLngBounds();

        places.forEach(function (place) {
            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }

            const marker = new google.maps.Marker({
                map: map,
                title: place.name,
                position: place.geometry.location
            });

            markers.push(marker);

            if (place.geometry.viewport) {
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }

            // Call your Blazor function using JavaScript Interop
            if (place.geometry) {
                const latitude = place.geometry.location.lat();
                const longitude = place.geometry.location.lng();
                if (mapId === "map1") {
                    DotNet.invokeMethodAsync("CarRentalACC", "SetCoordinatesMap1", latitude, longitude);
                } else if (mapId === "map2") {
                    DotNet.invokeMethodAsync("CarRentalACC", "SetCoordinatesMap2", latitude, longitude);
                }            }
        });

        map.fitBounds(bounds);
    }
}

function displayLocations(latitude1, longitude1, latitude2, longitude2) {
    const map1 = new google.maps.Map(document.getElementById('map1'), {
        center: { lat: latitude1, lng: longitude1 },
        zoom: 14
    });

    const map2 = new google.maps.Map(document.getElementById('map2'), {
        center: { lat: latitude2, lng: longitude2 },
        zoom: 14
    });

    const marker1 = new google.maps.Marker({
        map: map1,
        position: { lat: latitude1, lng: longitude1 }
    });

    const marker2 = new google.maps.Marker({
        map: map2,
        position: { lat: latitude2, lng: longitude2 }
    });
}
