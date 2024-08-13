import {
  UserGetListQuery,
  UserGetListReturn,
  UserGetReturn,
  UserPutBody,
} from './user-dto';
import {
  body,
  getHttpBackendClient,
  HttpBackendClient,
  queryParams,
  responseJson,
} from '@socnet/shared';

/** /api/v1/user */
class UserRepository {
  constructor(private _httpClient: HttpBackendClient) {}

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
  new UserRepository(getHttpBackendClient());
