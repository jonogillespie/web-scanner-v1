import {Component, Input, OnInit} from '@angular/core';


@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit {
  @Input() dataSource: any;
  displayedColumns: string[] = ['name', 'url', 'hasGoogleAnalytics', 'scanDuration', 'lastTimeScanned'];

  constructor() { }

  ngOnInit(): void {
  }

}
