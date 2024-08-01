import { inject, injectable } from 'inversify';
import {
  HttpBackendClient,
  TYPES,
  DIContainer,
  body,
  queryParams,
  responseJson,
  objectToFormData,
} from '@socnet/shared';
import {
  PostDeleteQuery,
  PostGetListQuery,
  PostGetListReturn,
  PostGetReturn,
  PostPostBody,
  PostPutBody,
} from './post-dto';

/** /api/v1/post */
@injectable()
export class PostRepository {
  constructor(
    @inject(TYPES.HttpBackendClient) private _httpClient: HttpBackendClient
  ) {}

  /** GET /api/v1/post/{id} */
  get(idPost: number) {
    return this._httpClient
      .get(`/api/v1/post/${idPost}`)
      .then(responseJson<PostGetReturn>);
  }
  /** DELETE /api/v1/post/{id} */
  delete(idPost: number, query: PostDeleteQuery) {
    return this._httpClient
      .delete(`/api/v1/post/${idPost}`, queryParams(query))
      .then(responseJson<void>);
  }

  /** GET /api/v1/post */
  getAll(query: PostGetListQuery) {
    return this._httpClient
      .get('/api/v1/post', queryParams(query))
      .then(responseJson<PostGetListReturn>);
  }
  /** POST /api/v1/post */
  post(payload: PostPostBody) {
    const formData = objectToFormData(payload);
    return this._httpClient
      .postForm('/api/v1/post', body(formData))
      .then(responseJson<number>);
  }

  /** PUT /api/v1/post */
  put(payload: PostPutBody) {
    const formData = objectToFormData(payload);
    return this._httpClient
      .putForm('/api/v1/post', body(formData))
      .then(responseJson<number>);
  }
}

export const getPostRepository = () =>
  DIContainer.get<PostRepository>(TYPES.PostRepository);
