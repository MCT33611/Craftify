import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import * as L from 'leaflet';
import { HttpClient } from '@angular/common/http';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-map-dialog',
  standalone:true,
  imports:[MaterialModule, CommonModule],
  template: `
    <h2 mat-dialog-title>Select Location</h2>
    <mat-dialog-content>
      <div id="map" style="height: 400px;"></div>
      <p *ngIf="selectedLocationName">Selected: {{ selectedLocationName }}</p>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button (click)="onCancel()">Cancel</button>
      <button mat-button [mat-dialog-close]="selectedLocation" [disabled]="!selectedLocation">Select</button>
    </mat-dialog-actions>
  `
})
export class MapDialogComponent implements OnInit {
  private map!: L.Map;
  private marker: L.Marker | null = null;
  selectedLocation: { lat: number, lng: number } | null = null;
  selectedLocationName: string = '';

  constructor(
    public dialogRef: MatDialogRef<MapDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { currentLocation: { lat: number, lng: number }, locationName: string },
    private http: HttpClient
  ) { }

  ngOnInit() {
    this.initMap();
  }

  initMap() {
    this.map = L.map('map').setView([this.data.currentLocation.lat, this.data.currentLocation.lng], 13);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '© OpenStreetMap contributors'
    }).addTo(this.map);

    this.setMarker(this.data.currentLocation.lat, this.data.currentLocation.lng);
    this.selectedLocationName = this.data.locationName;

    this.map.on('click', (e: L.LeafletMouseEvent) => {
      const { lat, lng } = e.latlng;
      this.setMarker(lat, lng);
      this.selectedLocation = { lat, lng };
      this.getLocationName(lat, lng);
    });
  }

  setMarker(lat: number, lng: number) {
    if (this.marker) {
      this.map.removeLayer(this.marker);
    }
    this.marker = L.marker([lat, lng]).addTo(this.map);
  }

  getLocationName(lat: number, lng: number) {
    this.http.get(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${lng}`)
      .subscribe((result: any) => {
        this.selectedLocationName = result.display_name;
      }, error => {
        console.error('Failed to get location name:', error);
      });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}