import { Injectable } from '@angular/core';
import { LocalStorageService } from 'angular-2-local-storage';

import { TMogSet } from '../core/models';

const CACHE_KEY = 'tmog-sets';
@Injectable()
export class TMogSetsCacheService {
    constructor(private localStorageService: LocalStorageService) {

    }

    public getAll(): TMogSet[] {
        return this.localStorageService.get<TMogSet[]>(CACHE_KEY);
    }

    public pop(id: number): TMogSet {
        const key = `${CACHE_KEY}[${id}]`;
        const returnValue = this.localStorageService.get<TMogSet>(key);

        this.localStorageService.remove(key);

        return returnValue;
    }

    public set(set: TMogSet): void;
    public set(sets: TMogSet[]): void;
    public set(item: TMogSet | TMogSet[]): void {
        if (Array.isArray(item)) {
            this.localStorageService.set(CACHE_KEY, item);
            return;
        }

        this.localStorageService.set(`${CACHE_KEY}[${item.id}]`, item);
    }

    public clear(): void {
        this.localStorageService.remove(CACHE_KEY);
    }
}
