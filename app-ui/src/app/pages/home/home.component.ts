import { Component, OnInit } from '@angular/core';
import { Resource } from 'src/app/models/Resource';
import { DataService } from 'src/app/services/data.service';
import { IOModel } from 'src/app/models/IOModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {

  resources: Resource[] = [];
  inputs: IOModel[] = [];
  outputs: IOModel[] = [];

  //private resourcesObservable: Observable<Resource[]>;

  constructor(private dataService: DataService) {
    //this.resourcesObservable = this.dataService.get_resources();
  }

  ngOnInit(): void {
    this.getResources();
    this.getInputs();

    this.getOutputs();
  }

  getResources() {
    this.dataService.get_resources().subscribe((res) => {
      this.resources = res;
    });
  }

  getInputs() {
    this.dataService.get_inputs().subscribe((res) => {
      this.inputs = res;
    });

    console.log(this.inputs);
  }

  getOutputs() {
    this.dataService.get_outputs().subscribe((res) => {
      this.outputs = res;
    });
  }

}
