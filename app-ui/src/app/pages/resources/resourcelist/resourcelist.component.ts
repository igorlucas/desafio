import { Component, OnInit } from '@angular/core';
import { Resource } from 'src/app/models/Resource';
import { DataService } from 'src/app/services/data.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { IOModel } from 'src/app/models/IOModel';



@Component({
  selector: 'app-resourcelist',
  templateUrl: './resourcelist.component.html',
  styleUrls: ['./resourcelist.component.sass']
})
export class ResourcelistComponent implements OnInit {
  resources: Resource[] = [];
  selectedResource: Resource;
  typeTrans: string;
  trans: IOModel;


  constructor(private dataService: DataService, private modalService: NgbModal) {
  }

  ngOnInit(): void {
    this.getResources();
  }

  open(content, obj) {
    if (obj) this.selectedResource = obj;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then((result) => {
        if (result == "delete") {
          this.dataService.delete_resource(this.selectedResource.id);
        } else if (this.typeTrans) {
          if (this.typeTrans == "input") {
            this.dataService.post_IOPuts(result, this.typeTrans + 's');
          } else if (this.typeTrans == "output") {
            this.dataService.post_IOPuts(result, this.typeTrans + 's');
          }
        }
        else if (this.selectedResource.id > 0) {
          this.dataService.put_resource(this.selectedResource);
        } else {
          this.dataService.post_resource(result);
        }

        this.getResources();
      }, (reason) => {
      });
  }

  

  getResources() {
    this.dataService.get_resources().subscribe((res) => {
      this.resources = res;
    });
  }
}