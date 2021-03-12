import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MapRestriction, LatLngBoundsLiteral, LatLngBounds } from '@agm/core/services/google-maps-types';
import { MouseEvent } from '@agm/core/map-types';

@Component({
  selector: 'app-geolocation-reader',
  templateUrl: './geolocation-reader.component.html',
  styleUrls: ['./geolocation-reader.component.scss'],
})
export class GeolocationReaderComponent implements OnInit {

  private canInit: boolean = true;

  @Input('mapInitialCoords')
  private mapInitialCoords: GeoCoords;

  @Input('mapRestriction')
  private mapRestriction: MapRestriction;

  @Input('markers')
  private markers: Array<MapMarker> = [new MapMarker(new GeoCoords(51.673858, 7.815982))];

  @Output('mapClick')
  private mapClick: EventEmitter<MapClickEvent> = new EventEmitter<MapClickEvent>();

  constructor ()
  { }

  ngOnInit ()
  { 
    
  }

  private onMapClick(ev: MouseEvent)
  {
    var coords = new GeoCoords(ev.coords.lat, ev.coords.lng);
    var mClickEv = new MapClickEvent(coords);
    this.mapClick.emit(mClickEv);
  }
}

export class GeoCoords
{
  private readonly _lng: number;
  private readonly _lat: number;

  constructor(lat: number, lng: number)
  {
    this._lng = lng;
    this._lat = lat;
  }

  get lng(): number { return this._lng; }
  get lat(): number { return this._lat; }
}

export class MapMarker
{
  private readonly _coords: GeoCoords;

  constructor(coords: GeoCoords) {
    this._coords = new GeoCoords(coords.lat, coords.lng);
  }

  get coords(): GeoCoords { return this._coords; }
}

export class MapClickEvent
{
  private readonly _coords: GeoCoords;

  constructor(coords: GeoCoords) {
    this._coords = new GeoCoords(coords.lat, coords.lng);
  }

  get coords(): GeoCoords { return this._coords; }
}
 