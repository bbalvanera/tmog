import * as winston from 'winston';

const format = winston.format.printf((info: any) => {
  return `${info.timestamp} ${(info.level + ':').padEnd(7)} ${info.message}`;
});

export const logger = winston.createLogger({
  level: 'info',
  format: winston.format.combine(winston.format.timestamp({ format: 'YYYY.MM.DD HH:mm:ss' }), format),
  transports: [
    new winston.transports.Console(),
    new winston.transports.File({
      filename: 'process.log',
      level: 'info',
      timestamp: false,
      json: false,
      showLevel: false
    })
  ]
});

export function log(message: any) {
  logger.info(message);
}
