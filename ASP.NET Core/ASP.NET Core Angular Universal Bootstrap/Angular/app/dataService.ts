import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

declare var params: any; // Params from browser

export class Data {
    Name: string;
}

@Injectable()
export class DataService {

    data: Data;

    constructor( @Inject('paramsData') paramsData: string) {
        // Default params
        this.data = new Data();
        this.data.Name = "Data from dataService.ts";
        // Browser params
        if (typeof params !== 'undefined') {
            this.data = JSON.parse(params.data);
        }
        // Angular universal params
        if (paramsData != null) {
            this.data = JSON.parse(paramsData);
        }
    }
}