import { injectable } from 'inversify';

@injectable()
export class EnvironmentService {
  getBackendUrl() {
    return 'http://26.38.50.104:8089/';
  }
}
