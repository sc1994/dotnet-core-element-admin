import request from '@/utils/request'

export function getRoles() {
  return request({
    url: '/role',
    method: 'get'
  })
}

export function addRole(data) {
  return request({
    url: '/role',
    method: 'post',
    data
  })
}

export function updateRole(id, data) {
  return request({
    url: `/role/${id}`,
    method: 'put',
    data
  })
}

export function deleteRole(id) {
  return request({
    url: `/role/${id}`,
    method: 'delete'
  })
}

export function getRoutes() {
  return request({
    url: '/role/getroutes',
    method: 'get'
  })
}
