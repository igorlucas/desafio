import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Resource } from '../models/Resource';
import { IOModel } from '../models/IOModel';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  baseUrl = 'https://localhost:44390/api';
  //baseUrl = 'http://localhost:3000';
  resources = [];

  constructor(private httpClient: HttpClient) { }

  // requests resources
  get_resources() {
    return this.httpClient.get<Resource[]>(this.baseUrl + '/resources');
  }

  get_resourceById(id: number) {
    return this.httpClient.get<Resource>(`${this.baseUrl}/resources/${id}`);
  }

  post_resource(data: any) {
    return this.httpClient.post(this.baseUrl + '/resources', data).subscribe(res => {
      console.log("POST Ok", res);
    }, error => {
      console.log("POST Error", error);
    });
  }

  put_resource(data: any) {
    return this.httpClient.put(`${this.baseUrl}/resources/${data.id}`, data)
      .subscribe(res => {
        console.log("PUT Ok", res);
      }, error => {
        console.log("PUT Error", error);
      });
  }

  patch_resource(data: any) {
    return this.httpClient.patch(`${this.baseUrl}/resources/${data.id}`, data)
      .subscribe(res => {
        console.log("PUT Ok", res);
      }, error => {
        console.log("PUT Error", error);
      });
  }

  delete_resource(id: any) {
    return this.httpClient.delete(`${this.baseUrl}/resources/${id}`)
      .subscribe(res => {
        console.log("DELETE Ok", res);
      }, error => {
        console.log("DELETE Error", error);
      });
  }

  //requests IO
  get_inputs() {
    return this.httpClient.get<IOModel[]>(this.baseUrl + '/inputs');
  }

  get_outputs() {
    return this.httpClient.get<IOModel[]>(this.baseUrl + '/outputs');
  }

  post_IOPuts(data: IOModel, type: string) {
    return this.httpClient.post(`${this.baseUrl}/${type}`, data).subscribe(res => {
      console.log("POST Ok", res);
    }, error => {
      console.log("POST Error", error);
    });
  }

}
