import axios, { Axios } from 'axios';
import { inject, injectable } from 'inversify';
import { DIContainer, TYPES } from '../di-container';
import { EnvironmentService } from '../environment-service';
import { body, joinUrl, queryParams } from '../http';
import { LoginData, SignUpData } from './auth-types';
import { AUTH_TOKEN } from '../../consts';

@injectable()
export class AuthService {
  private axiosInstance: Axios | null = null;
  public constructor(
    @inject(TYPES.EnvReader) private _envReader: EnvironmentService
  ) {}

  private get _authUrl() {
    return joinUrl(this._envReader.getBackendUrl(), 'api/v1/auth');
  }

  async logIn(credentials: LoginData) {
    const response = await axios.get<string>(
      this._authUrl,
      queryParams(credentials)
    );
    localStorage.setItem(AUTH_TOKEN, response.data);
  }

  async signUp(signUpData: SignUpData) {
    const response = await axios.post<string>(this._authUrl, body(signUpData));

    localStorage.setItem(AUTH_TOKEN, response.data);
  }

  logOut() {
    localStorage.removeItem(AUTH_TOKEN);
  }

  getAxiosClient() {
    if (this.axiosInstance) {
      return this.axiosInstance;
    }
    this.axiosInstance = new Axios();
    this.axiosInstance.interceptors.request.use(async (config) => {
      const token = localStorage.getItem(AUTH_TOKEN);
      config.headers.setContentType('application/json');
      if (token) {
        config.headers.setAuthorization(`Bearer ${token}`);
      }
      return config;
    });

    return this.axiosInstance;
  }
}

export const getAuthService = () =>
  DIContainer.get<AuthService>(TYPES.AuthClient);
