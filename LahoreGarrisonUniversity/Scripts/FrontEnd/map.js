var map;
jQuery(document).ready(function () {

    map = new GMaps({
        div: '#map',
        lat: 31.49893,
        lng: 74.42239,
    });

    //map.addMarker({
    //    lat: 51.451573,
    //    lng: -2.595008,
    //    title: 'Address',
    //    infoWindow: {
    //        content: '<h5 class="title">College Green Campus</h5><p><span class="region">Address line goes here</span><br><span class="postal-code">Postcode</span><br><span class="country-name">Country</span></p>'
    //    }
    //});

    //map.addMarker({

    //    lat: 31.51327,
    //    lng: 74.42282,
    //    title: 'Address1',
    //    infoWindow: {
    //        content: '<h5 class="title">College Green Campus</h5><p><span class="region">Address line goes here</span><br><span class="postal-code">Postcode</span><br><span class="country-name">Country</span></p>'
    //    }
    //});
});