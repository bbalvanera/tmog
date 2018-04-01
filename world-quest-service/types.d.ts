declare module "winston" {
    export const format: any;
    export const transports: any;
    export const createLogger: (options: any) => Logger;
}

declare type Logger = {
    log: (level: string, message: string, metadata: any) => void;
    info: logging;
    error: logging;
    warn: logging;
}

declare type logging = (message: string) => void;