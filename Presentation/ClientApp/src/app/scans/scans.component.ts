import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-scans',
  templateUrl: './scans.component.html',
  styleUrls: ['./scans.component.scss']
})
export class ScansComponent implements OnInit {
  dataSource: any;
  latestScanNotFound = false;

  constructor(@Inject('BASE_URL') private baseUrl: string,
              private http: HttpClient) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.http.get(`${this.baseUrl}api/scan-cycles/latest`)
      .subscribe(res => {
        this.dataSource = res;
        this.latestScanNotFound = false;
      }, err => {
        this.latestScanNotFound = err.status === 404;
      });
  }

  onRefreshButtonClick(e: MouseEvent): void {
    this.loadData();
  }
}
