import {catchError,  map } from 'rxjs/operators';
import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from '../../../shared/services/base.service';
import { WindowRefService } from '../../../shared/services/windowref.service';
import { Role } from '../../../entities/role';

@Injectable()
export class RolesService extends BaseService {
    roleUrl: string;

    constructor(
        private http: HttpClient,
        @Inject(WindowRefService) windowRefService: WindowRefService,
        @Inject(PLATFORM_ID) platformId: Object,
        @Inject('BASE_URL') private baseUrl: string
    ) {
        super(windowRefService, platformId);
        this.roleUrl = `${this.baseUrl}/api/Roles`;
    }

    private addResult(res: any) {
        var id = 0;
        var output;
        if (res.data.addSection) {
            id = res.data.addSection.id;
            output = res.data.addSection;
        }
        if (res.data.addSectionItem) {
            id = res.data.addSectionItem.id;
            output = res.data.addSectionItem;
        }

        if (id === 0){
            throw new Error('Unable to create object.');
        }
        return output || {};
    }

    private extractData(res: Response) {
        if (res.status < 200 || res.status >= 300) {
            throw new Error('Response status: ' + res.status);
        }
        let body = res.json();
        return body || {};
    }

    public getAll(): Observable<Role[]> {
        return this.http.get<Role[]>(this.baseUrl + "api/Roles/GetAll", this.jsonAuthRequestOptions()).pipe(
        catchError(this.handleError));
    }

    public get(id: string): Observable<Role> {
        return this.http.get<Role>(this.baseUrl + "api/Roles/Get", this.jsonAuthRequestOptions()).pipe(
        catchError(this.handleError));
    }

    public addOrUpdate(
        id: string,
        name: string
    ): Observable<boolean> {
        if (!id) {
            return this.add(
                name
            );
        } else {
            return this.update(
                id,
                name
            );
        }
    }

    public add(
        name: string
    ): Observable<boolean> {
        let body = JSON.stringify({ 
            name
        });
        return this.http.post(this.baseUrl + "api/Roles/Add", body, this.jsonRequestOptions).pipe(
        map(res => true),
        catchError(this.handleError),);
    }

    public update(
        id: string,
        name: string
    ): Observable<boolean> {
        let body = JSON.stringify({ 
            id,
            name
        });
        return this.http.post(this.baseUrl + "api/Roles/Update", body, this.jsonRequestOptions).pipe(
        map(res => true),
        catchError(this.handleError),);
    }

    public delete(
        id: string
    ): Observable<boolean> {
        return this.http.post(this.baseUrl + "api/Roles/Delete", id, this.jsonRequestOptions).pipe(
        map(res => true),
        catchError(this.handleError),);
    }
}