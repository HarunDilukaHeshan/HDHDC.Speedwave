import { GeoCoords } from '../components/geolocation-reader/geolocation-reader.component';

export class GeoCoordsParser {
    static parse(value: string): GeoCoords {
        try {
            var geoCoords = value.split(':');
            var lat = parseFloat(geoCoords[0] as string);
            var lng = parseFloat(geoCoords[1] as string);

            var lat_r = parseFloat(lat.toFixed(8));
            var lng_r = parseFloat(lng.toFixed(8));

            return new GeoCoords(lat_r, lng_r);
        }
        catch(e) { return null; }
    }
}