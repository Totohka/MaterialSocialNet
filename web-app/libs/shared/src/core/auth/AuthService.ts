import axios, { Axios } from 'axios';
import {
  EnvironmentService,
  getEnvironmentService,
} from '../environment-service';
import { body, joinUrl, queryParams } from '../http';
import { LoginData, SignUpData } from './auth-types';
import { AUTH_TOKEN } from '../../consts';
import { StorageInterface } from '../storage/storage-interface';
import { getLocalStorage } from '../storage/local-storage';

export class AuthService {
  private axiosInstance: Axios | null = null;
  public constructor(
    private _envReader: EnvironmentService,
    private _storage: StorageInterface
  ) {}

  private get _authUrl() {
    return joinUrl(this._envReader.getBackendUrl(), 'api/v1/auth');
  }

  async logIn(credentials: LoginData) {
    const response = await axios.get<string>(
      this._authUrl,
      queryParams(credentials)
    );
    this._storage.setAccessToken(response.data);
  }

  async signUp(signUpData: SignUpData) {
    const response = await axios.post<string>(this._authUrl, body(signUpData));

    this._storage.setAccessToken(response.data);
  }

  logOut() {
    this._storage.removeTokens();
  }

  getAxiosClient() {
    if (this.axiosInstance) {
      return this.axiosInstance;
    }
    this.axiosInstance = new Axios();
    this.axiosInstance.interceptors.request.use(async (config) => {
      const token = this._storage.getAccessToken();
      config.headers.setContentType('application/json');
      if (token) {
        config.headers.setAuthorization(`Bearer ${token}`);
      }
      return config;
    });

    return this.axiosInstance;
  }

  /** Получение access_token. Если токена нет будет выполнена попытка обновить его.
   * Если refresh_token тоже нет, а `tryLogin` == true, пользователь будет отправлен
   * на страницу с логированием; в противном случае вернется пустая строка. */
  async getAccessToken(tryLogin = true): Promise<string> {
    const accessToken = this._storage.getAccessToken();
    if (accessToken) {
      return accessToken;
    }
    // const refreshToken = this._storage.getRefreshToken();
    // if (refreshToken) {
    //   return this.updateAccessToken();
    // }
    if (tryLogin) {
      throw new Error("Can't get access token");
    }
    return '';
  }
}

export const getAuthService = () =>
  new AuthService(getEnvironmentService(), getLocalStorage());
