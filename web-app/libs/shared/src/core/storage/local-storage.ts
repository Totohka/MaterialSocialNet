import { AUTH_TOKEN } from '../../consts';
import { StorageInterface } from './storage-interface';

export class LocalStorage implements StorageInterface {
  getAccessToken() {
    return localStorage.getItem(AUTH_TOKEN);
  }
  setAccessToken(token: string): void {
    localStorage.setItem(AUTH_TOKEN, token);
  }
  removeTokens(): void {
    localStorage.removeItem(AUTH_TOKEN);
  }
}

export const getLocalStorage = () => new LocalStorage();
