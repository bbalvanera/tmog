export interface WorkCommand {
  execute(): Promise<void>;
}