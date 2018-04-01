import { Zone } from "../entities/Zone";
import * as sql from 'mssql';

const config = {
  user: 'sa',
  password: 'sa',
  server: 'localhost\\SQLEXPRESS', // You can use 'localhost\\instance' to connect to named instance
  database: 'TMogDev'
};

export class ZoneData {
  private records: sql.Table;
  
  constructor() {
    this.createZonesTable();
  }

  public add(zone: Zone): void {
    if (zone) {
      this.records.rows.add(zone.id, zone.name, zone.type, zone.category);
    }
  }

  public async saveChanges(): Promise<void> {
    const pool = new sql.ConnectionPool(config);

    await pool.connect();
    await pool.request().execute('dbo.ClearZonesRaw');
    await pool.request().bulk(this.records);

    pool.close();
  }

  private createZonesTable() {
    const table = new sql.Table('_zonesraw');

    table.create = true;
    table.columns.add('id', sql.Int, { nullable: false });
    table.columns.add('name', sql.VarChar(sql.MAX), { nullable: true });
    table.columns.add('type', sql.VarChar(sql.MAX), { nullable: true });
    table.columns.add('category', sql.VarChar(sql.MAX), { nullable: true });

    this.records = table;
  }
}