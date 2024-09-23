var map = L.map('map').setView([39.9286946, 32.81890], 15);

var googleBaseMap = L.tileLayer('https://{s}.google.com/vt/lyrs=m&x={x}&y={y}&z={z}', {
    attribution: "proje1",
    maxZoom: 20,
    subdomains: ['mt0', 'mt1', 'mt2', 'mt3']
}).addTo(map);

var wmsLayer = L.tileLayer.wms('https://ows.terrestris.de/osm/service?', {
    layers: 'OSM-WMS',
    format: 'image/png',
    transparent: true,
    attribution: "WMS Katmanı"
});

let wmsLayerVisible = false; 

document.getElementById('toggle-wms-btn').addEventListener('click', function () {
    if (wmsLayerVisible) {
        map.removeLayer(wmsLayer);
    } else {
        map.addLayer(wmsLayer);
    }
    wmsLayerVisible = !wmsLayerVisible;
});

let featureGroup = L.featureGroup().addTo(map);

let isAddingMarker = false; 
let isAddingPolygon = false; 
let isAddingCircle = false; 
let isAddingPolyline = false; 
let isAddingRectangle = false; 
let isAddingImportantPoint = false; 

let markers = [];
let polygons = [];
let circles = [];
let polylines = [];
let rectangles = [];
let polylinePoints = [];
let importantPoints = [];
let rectanglePoints = [];

document.getElementById('add-marker-btn').addEventListener('click', function () {
    setMode('marker');
});

document.getElementById('add-polygon-btn').addEventListener('click', function () {
    setMode('polygon');
    polygonPoints = []; 
});

document.getElementById('add-circle-btn').addEventListener('click', function () {
    setMode('circle');
});

document.getElementById('add-polyline-btn').addEventListener('click', function () {
    setMode('polyline');
    polylinePoints = []; 
});

document.getElementById('add-rectangle-btn').addEventListener('click', function () {
    setMode('rectangle');
    rectanglePoints = []; 
});

document.getElementById('add-important-point-btn').addEventListener('click', function () {
    setMode('importantPoint');
});

function setMode(mode) {
    isAddingMarker = mode === 'marker';
    isAddingPolygon = mode === 'polygon';
    isAddingCircle = mode === 'circle';
    isAddingPolyline = mode === 'polyline';
    isAddingRectangle = mode === 'rectangle';
    isAddingImportantPoint = mode === 'importantPoint';
    alert(`Haritada ${mode} eklemek için mod aktif. Sağ tıklayarak silme yapabilirsiniz. Çift tıklayarak ekleme işlemini bitirebilirsiniz. Üzerine tıklayarak not bırakabilirsiniz`);
}

map.on('click', function (e) {
    if (isAddingMarker) {
        addMarker(e.latlng);
    } else if (isAddingPolygon) {
        addPolygonPoint(e.latlng);
    } else if (isAddingCircle) {
        addCircle(e.latlng);
    } else if (isAddingPolyline) {
        addPolylinePoint(e.latlng);
    } else if (isAddingRectangle) {
        addRectanglePoint(e.latlng);
    } else if (isAddingImportantPoint) {
        addImportantPoint(e.latlng);
    }
});

function addMarker(latlng) {
    let newMarker = L.marker(latlng);
    let markerData = {
        latlng: latlng,
        note: '' 
    };


    addNoteToFeature(newMarker, "Notunuzu girin:", function (userNote) {
        markerData.note = userNote; 
    });

    newMarker.on('contextmenu', function () {
        featureGroup.removeLayer(newMarker);
        markers = markers.filter(m => m !== markerData); 
    });

    markers.push(markerData); 
    featureGroup.addLayer(newMarker);
    isAddingMarker = false;
}


function addPolygonPoint(latlng) {
    polygonPoints.push(latlng);
    if (polygonPoints.length >= 2) {
        let polygon = L.polygon(polygonPoints);
        addNoteToFeature(polygon, "Notunuzu girin:", ""); 
        polygon.on('contextmenu', function () {
            featureGroup.removeLayer(polygon);
            polygons = polygons.filter(p => p !== polygon);
        });

        polygons.push({
            points: polygonPoints,
            note: ''
        });
        featureGroup.addLayer(polygon);

        if (polygonPoints.length >= 3) {
            polygonPoints = [];
            isAddingPolygon = false;
        }
    }
}


function addCircle(latlng) {
    let radius = 100;
    let newCircle = L.circle(latlng, { radius });
    addNoteToFeature(newCircle, "Notunuzu girin:", ""); 
    newCircle.on('contextmenu', function () {
        featureGroup.removeLayer(newCircle);
        circles = circles.filter(c => c !== newCircle);
    });

    circles.push({
        center: latlng,
        radius: radius,
        note: ''
    });
    featureGroup.addLayer(newCircle);
    isAddingCircle = false;
}


function addPolylinePoint(latlng) {
    polylinePoints.push(latlng);
    if (polylinePoints.length >= 2) {
        let polyline = L.polyline(polylinePoints);
        addNoteToFeature(polyline, "Notunuzu girin:", "");
        polyline.on('contextmenu', function () {
            featureGroup.removeLayer(polyline);
            polylines = polylines.filter(p => p !== polyline);
        });

        polylines.push({
            points: polylinePoints,
            note: ''
        });
        featureGroup.addLayer(polyline);

        polylinePoints = [];
        isAddingPolyline = false;
    }
}


function addRectanglePoint(latlng) {
    rectanglePoints.push(latlng);
    if (rectanglePoints.length === 2) {
        let bounds = L.latLngBounds(rectanglePoints);
        let rectangle = L.rectangle(bounds);
        addNoteToFeature(rectangle, "Notunuzu girin:", "");
        rectangle.on('contextmenu', function () {
            featureGroup.removeLayer(rectangle);
            rectangles = rectangles.filter(r => r !== rectangle);
        });

        rectangles.push({
            bounds: bounds,
            note: ''
        });
        featureGroup.addLayer(rectangle);

        rectanglePoints = [];
        isAddingRectangle = false;
    }
}


function addImportantPoint(latlng) {
    let importantPoint = L.marker(latlng, { icon: L.icon({iconUrl: 'images/importantPoint.png',  iconSize: [32, 32]}) });
    addNoteToFeature(importantPoint, "Notunuzu girin:", ""); 
    importantPoint.on('contextmenu', function () {
        featureGroup.removeLayer(importantPoint);
        importantPoints = importantPoints.filter(p => p !== importantPoint);
    });

    importantPoints.push({
        latlng: latlng,
        note: ''
    });
    featureGroup.addLayer(importantPoint);
    isAddingImportantPoint = false;
}

function addNoteToFeature(feature, promptText, defaultText) {
    let note = defaultText || ''; 

    feature.on('click', function () {
        let content = prompt(promptText, note);
        if (content !== null) {
            note = content.trim();
            if (note) {
                feature.bindTooltip(note, { permanent: true, direction: 'top' }).openTooltip();
            } else {
                feature.unbindTooltip(); 
            }
            feature.note = note;
        }
    });
}


document.getElementById('save-map').addEventListener('click', function () {
    if (markers.length === 0 && polygons.length === 0 && circles.length === 0 &&
        polylines.length === 0 && rectangles.length === 0 && importantPoints.length === 0) {
        alert("Haritada hiçbir özellik yok.");
        return; 
    }

    let savedData = {
        mapData: {
            markers: markers.map(marker => ({
                lat: marker.latlng.lat,
                lng: marker.latlng.lng,
                note: marker.note || ''
            })),
            polygons: polygons.map(polygon => ({
                points: polygon.points.map(point => ({
                    latitude: point.lat,
                    longitude: point.lng
                })),
                note: polygon.note || ''
            })),
            circles: circles.map(circle => ({
                lat: circle.center.lat,
                lng: circle.center.lng,
                radius: circle.radius,
                note: circle.note || ''
            })),
            polylines: polylines.map(polyline => ({
                points: polyline.points.map(point => ({
                    latitude: point.lat,
                    longitude: point.lng
                })),
                note: polyline.note || ''
            })),
            rectangles: rectangles.map(rectangle => ({
                northeast: {
                    latitude: rectangle.bounds.getNorthEast().lat,
                    longitude: rectangle.bounds.getNorthEast().lng
                },
                southwest: {
                    latitude: rectangle.bounds.getSouthWest().lat,
                    longitude: rectangle.bounds.getSouthWest().lng
                },
                note: rectangle.note || ''
            })),
            importantPoints: importantPoints.map(importantPoint => ({
                lat: importantPoint.latlng.lat,
                lng: importantPoint.latlng.lng,
                note: importantPoint.note || ''
            }))
        }
    };
    
    

    console.log("Gönderilen veri:", JSON.stringify(savedData));

    fetch('https://localhost:7049/api/values', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer <your-auth-token>'
        },
        body: JSON.stringify(savedData)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Ağ hatası: ' + response.statusText);
        }
        return response.json();
    })
    .then(result => {
        alert('Veriler başarıyla kaydedildi!');
        console.log(result);
    })
    .catch(error => {
        console.error('Veri kaydetme hatası:', error);
        alert('Veri kaydedilirken bir hata oluştu.');
    });
});


function clearAllFeatures() {
    featureGroup.clearLayers(); 
    markers = [];
    polygons = [];
    circles = [];
    polylines = [];
    rectangles = [];
    importantPoints = [];
    alert("Haritadaki tüm objeler temizlendi!");
}

document.getElementById('remove-all-map-btn').addEventListener('click', function () {
    featureGroup.clearLayers();
    markers = [];
    polygons = [];
    circles = [];
    polylines = [];
    rectangles = [];
    importantPoints = [];
    alert("Haritadaki tüm objeler temizlendi!");
});


function displayDataInModal(data) {
    const tbody = document.querySelector('#dataModalMap tbody');
    tbody.innerHTML = '';

    data.forEach((item, index) => {
        const row = `<tr>
            <th scope="row">${index + 1}</th>
            <td>${item.id}</td>
            <td>${item.markers.length > 0 ? 'Marker' : ''} ${item.polygons.length > 0 ? ', Polygon' : ''} ${item.circles.length > 0 ? ', Circle' : ''} ${item.polylines.length > 0 ? ', Polyline' : ''} ${item.rectangles.length > 0 ? ', Rectangle' : ''} ${item.importantPoints.length > 0 ? ', Important Point' : ''}</td>
            <td><button class="btn btn-secondary" onclick="getData(${item.id})">Getir</button></td> <!-- Buton -->
        </tr>`;
        tbody.innerHTML += row; 
    });
}


function getData(id) {
    fetch(`https://localhost:7049/api/values/${id}`)
        .then(response => response.json())
        .then(data => {
            displayDataInModal(data);
        })
        .catch(error => console.error('Veri alınırken hata:', error));
}



async function fetchMapData(id) {
    const response = await fetch(`https://localhost:7049/api/values/${id}`);

    if (!response.ok) {
        console.error('Veri alınamadı:', response.statusText);
        return;
    }

    const data = await response.json();
    displayShapesOnMap(data); 
}



document.getElementById('showShapesBtn').addEventListener('click', function() {
    clearAllFeatures(); 
    fetchAndDisplayShapes(); 
});

async function fetchAndDisplayShapes(id) {
    const response = await fetch(`https://localhost:7049/api/values/${id}`);

    if (!response.ok) {
        console.error('Veri alınamadı:', response.statusText);
        return;
    }

    const data = await response.json();
    displayShapesOnMap(data);  
}

 function displayShapesOnMap(data) {
     data.markers.forEach(marker => {
         L.marker([marker.lat, marker.lng]).addTo(map)
            .bindPopup(marker.note);
    });

    data.polygons.forEach(polygon => {
         const latlngs = polygon.points.map(point => [point.latitude, point.longitude]);
        L.polygon(latlngs).addTo(map);
    });

    data.circles.forEach(circle => {
         L.circle([circle.lat, circle.lng], {
            radius: circle.radius  
        }).addTo(map);
    });

    data.polylines.forEach(polyline => {
         const latlngs = polyline.points.map(point => [point.latitude, point.longitude]);
        L.polyline(latlngs).addTo(map);
    });

    data.rectangles.forEach(rectangle => {
         const bounds = [
            [rectangle.southwest.latitude, rectangle.southwest.longitude],
            [rectangle.northeast.latitude, rectangle.northeast.longitude]
        ];
        L.rectangle(bounds).addTo(map);
    });

    data.importantPoints.forEach(point => {
    
        const specialIcon = L.icon({
            iconUrl: 'images/importantPoint.png', 
            iconSize: [32, 32] 
        });
        
        L.marker([point.lat, point.lng], { icon: specialIcon }).addTo(map)
            .bindPopup(point.note);
    });
}

async function fetchMapData() {
    const response = await fetch('https://localhost:7049/api/values');  

    if (!response.ok) {
        console.error('Veri alınamadı:', response.statusText);
        return;
    }

    const data = await response.json();
    displayDataInModal(data);
}

function displayDataInModal(data) {
    const tbody = document.querySelector('#dataModalMap tbody');
    tbody.innerHTML = ''; 

    data.forEach((item, index) => {
        const row = `<tr>
            <th scope="row">${index + 1}</th>
            <td>${item.id}</td>
            <td>${item.markers.length > 0 ? 'Marker' : ''} ${item.polygons.length > 0 ? ', Polygon' : ''} ${item.circles.length > 0 ? ', Circle' : ''} ${item.polylines.length > 0 ? ', Polyline' : ''} ${item.rectangles.length > 0 ? ', Rectangle' : ''} ${item.importantPoints.length > 0 ? ', Important Point' : ''}</td>
            <td>
                <button class="btn btn-primary showShapesBtn" data-id="${item.id}">Haritaya Aktar</button>
                <button class="btn btn-danger removeBtn" data-id="${item.id}">Sil</button>
            </td>
        </tr>`;
        tbody.innerHTML += row; 
    });

    
    const buttons = document.querySelectorAll('.showShapesBtn');
    
    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const id = button.getAttribute('data-id'); 
            fetchAndDisplayShapes(id); 
        });
    });
    const removeButtons = document.querySelectorAll('.removeBtn');
    removeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const id = button.getAttribute('data-id');
            deleteMapData(id);
        });
    });
}
function deleteMapData(id) {
    fetch(`https://localhost:7049/api/values/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': 'Bearer <your-auth-token>'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Silme hatası: ' + response.statusText);
        }
        alert('Veri başarıyla silindi!');
        fetchMapData(); 
    })
    .catch(error => {
        console.error('Silme işlemi hatası:', error);
        alert('Silme işlemi sırasında bir hata oluştu.');
    });
}
document.getElementById('get-map-data').addEventListener('click', fetchMapData);
