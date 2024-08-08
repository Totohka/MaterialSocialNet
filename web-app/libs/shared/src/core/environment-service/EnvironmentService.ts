export class EnvironmentService {
  getBackendUrl() {
    return 'http://26.38.50.104:8089/';
  }

  getHubUrl() {
    return {
      notification: 'http://25.32.11.98:8090/notification',
      chat: 'http://25.32.11.98:8089/chat',
    };
  }
}

export const getEnvironmentService = () => new EnvironmentService();
