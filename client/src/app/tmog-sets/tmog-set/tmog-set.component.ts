import { Component } from '@angular/core';

@Component({
  moduleId: module.id,
  selector: 'tmog-set',
  templateUrl: 'tmog-set.component.html',
  styleUrls: ['tmog-set.component.css']
})
export class TMogSetComponent {
    public slots: any = [
        {
            name: 'Head',
            complete: false,
            items: [
                {
                    name: 'Helm of Might',
                    source: 'Unavailable'
                },
                {
                    name: 'Burnished Helm of Might',
                    source: 'Vendor, Achievement'
                },
                {
                    name: 'Faceguard of the Crown',
                    source: 'A Dangerous Alliance'
                }
            ]
        },
        {
            name: 'Shoulder',
            complete: true,
            items: [
                {
                    name: 'Burnished Pauldrons of Might',
                    source: {
                        description: 'Unavailable',
                        zone: {
                            id: 1,
                            name: 'Molten Core'
                        }
                    }
                },
                {
                    name: 'Burnished Pauldrons of Might',
                    source: {
                        description: 'Vendors'
                    }
                }
            ]
        }
    ];
}
