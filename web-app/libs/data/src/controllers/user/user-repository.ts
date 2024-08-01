import { inject, injectable } from 'inversify';
import {
  HttpBackendClient,
  TYPES,
  DIContainer,
  body,
  queryParams,
  responseJson,
} from '@socnet/shared';
import {
  UserGetListQuery,
  UserGetListReturn,
  UserGetReturn,
  UserPutBody,
} from './user-dto';

/** /api/v1/user */
@injectable()
export class UserRepository {
  constructor(
    @inject(TYPES.HttpBackendClient) private _httpClient: HttpBackendClient
  ) {}

  // TODO: Реализовать метод post и delete, когда будет готов бэк

  /** PUT /api/user */
  put(params: UserPutBody) {
    return this._httpClient
      .put('/api/v1/user', body(params))
      .then(responseJson<string>);
  }

  /** GET /api/user */
  getAll(query: UserGetListQuery) {
    return this._httpClient
      .get('/api/v1/user', queryParams(query))
      .then(responseJson<UserGetListReturn>);
  }

  /** GET /api/user/{id} */
  get(userId: string) {
    return this._httpClient
      .get(`/api/v1/user/${userId}`)
      .then(responseJson<UserGetReturn>);
  }
}

export const getUserRepository = () =>
  DIContainer.get<UserRepository>(TYPES.UserRepository);
